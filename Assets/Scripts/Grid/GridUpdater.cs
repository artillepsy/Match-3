using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;

namespace Grid
{
    public class GridUpdater : MonoBehaviour
    {
        [SerializeField] private CellItem itemPrefab;
        
        
        public void ReformGrid()
        {
            // вызывается, когда на поле не может быть матчей
        }

        public void UpdateGrid()
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
                    

                    do 
                    {
                        pos.y -= step;
                    }while(busyPoints.Contains(pos));
                    
                    busyPoints.Add(pos);
                    
                    var item = Instantiate(itemPrefab, pos, Quaternion.identity, grid[i, j].transform);

                    grid[i, j].Item = item;
                     
                    item.SetVariant(GridContainer.Inst.Variant);
                     
                   // Debug.Log("ID: "+ grid[i, j].Item.Id);
                    
                    ItemMover.Inst.MoveItem(grid[i, j]);
                }
            }
        }

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener((cellsToClear) =>
            {
                //Debug.Log("Invoked in gridUpdater");

                //Debug.Break();
                
                ClearCells(cellsToClear);
                
               // Debug.Log("Cleared cells");

               // Debug.Break();
                
                UpdateGrid();
                
                //Debug.Log("Updated Grid");

                //Debug.Break();
            });
        }

        private void ClearCells(List<Cell> cells)
        {
            //Debug.Log("cell count: "+ cells.Count);
            
            foreach (var cell in cells)
            {
                var item = cell.Item;

                Debug.Log("Deleted: ("+cell.X+", "+cell.Y+"). Item: "+ item);
              
                item.transform.SetParent(null);
                
                cell.Item = null;
                
                Destroy(item.gameObject);
            }
        }
    }
}