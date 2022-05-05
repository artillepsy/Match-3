using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;

namespace Grid
{
    public class GridUpdater : MonoBehaviour
    {
       // public static UnityEvent OnGridUpdated = new UnityEvent();
        
        public void ReformGrid()
        {
            // вызывается, когда на поле не может быть матчей
        }

        public void UpdateGrid(List<Cell> cellsToClear)
        {
            var reserved = new List<Vector2>();
            var grid = GridContainer.Inst.Grid;
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            
            ClearCells(cellsToClear);

            for (var j = 0; j < y; j++)
            {
                for (var i = 0; i < x; i++)
                {
                    if (grid[i, j].Empty || grid[i, j].Item) continue;

                    int k;

                    for (k = j; k < y; k++)
                    {
                        if (grid[i, k].Empty || !grid[i, k].Item) continue;
                        
                        ItemMover.Inst.MoveItem(grid[i, k], grid[i, j]);

                        break;
                    }
                    
                    if (k < y) continue;

                    var pos = (Vector2)grid[i, y - 1].transform.position;
                    
                    var step = GridContainer.Inst.Ystep;

                    while(reserved.Contains(pos))
                    {
                        pos.y += step;
                    }
                    
                    grid[i, j].SetVariant(GridContainer.Inst.Variant);
                    
                    ItemMover.Inst.MoveItem(grid[i, j], pos);
                }
            }
        }

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener(UpdateGrid);
        }

        private void ClearCells(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                var item = cell.Item;
                
                Destroy(item.gameObject);
                
                item.transform.SetParent(null);

                cell.Item = null;
            }
        }
    }
}