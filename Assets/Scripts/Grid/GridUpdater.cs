using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    /// <summary>
    /// Класс, удаляющий три+ в ряд элементы и
    /// заполняющий сетку снизу 
    /// </summary>
    public class GridUpdater : MonoBehaviour
    {
        [SerializeField] private CellItem itemPrefab;

        public static UnityEvent<List<int>> OnCachedClearedIds = new UnityEvent<List<int>>();
        /// <summary>
        /// Обновление сетки после удаления в ней
        /// собранных три+ в ряд элементов.
        /// Вызывается после того, как в сетке
        /// были найдены три+ в ряд элементы
        /// </summary>
        private void UpdateGrid()
        {
            var busyPoints = new List<Vector2>();
            var grid = GridContainer.Inst.Grid;
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            var step = GridContainer.Inst.Ystep;

            for (var j = 0; j < y; j++)
            {
                for (var i = 0; i < x; i++)
                {
                    if (grid[i, j].Empty || grid[i, j].Item) continue;
                    var shouldInst = true;

                    for (var k = j + 1; k < y; k++)
                    {
                        if (grid[i, k].Empty || !grid[i, k].Item) continue;
                        ItemMover.Inst.MoveItem(grid[i, k], grid[i, j]);
                        shouldInst = false;
                        break;
                    }
                    if (!shouldInst) continue;

                    var pos = (Vector2) grid[i, y - 1].transform.position;

                    while (true)
                    {
                        pos.y -= step;
                        if (!busyPoints.Contains(pos)) break;
                    }
                    busyPoints.Add(pos);
                    var item = Instantiate(itemPrefab, pos, Quaternion.identity, grid[i, j].transform);
                    grid[i, j].Item = item;
                    item.SetVariant(GridContainer.Inst.Variant);
                    ItemMover.Inst.MoveItem(grid[i, j]);
                }
            }
        }

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener((cellsToClear) =>
            {
                ClearCells(cellsToClear);
                UpdateGrid();
            });
        }
        /// <summary>
        /// Удаление всех собранных три+ в ряд элементов
        /// </summary>
        private void ClearCells(List<Cell> cells)
        {
            var ids = new List<int>();
            foreach (var cell in cells)
            {
                var item = cell.Item;
                if (!item)
                {
                    Debug.LogError("Del: ("+cell.X+", "+cell.Y+"). Item: null "+ item);
                    Debug.Break();
                    continue;
                }
                ids.Add(item.Id);
                item.transform.SetParent(null);
                cell.Item = null;
                Destroy(item.gameObject);
            }
            OnCachedClearedIds?.Invoke(ids);
        }
    }
}