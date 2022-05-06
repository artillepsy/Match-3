using System.Collections.Generic;
using Cells;
using UnityEngine;

namespace Grid
{
    public class GridReformer : MonoBehaviour
    {
        public void ReformGridWithNoMatches()
        {
            
        }
        public void ReformGrid()
        {
            // вызывается, когда на поле не может быть матчей. ПЕРЕСТАВЛЯЕТ старые элементы
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            var grid = GridContainer.Inst.Grid;
            var cells = GridContainer.Inst.GetAllCells();

            foreach (var cell in grid)
            {
                
            }
            
            // проверить на НЕ матчи
            
            // в конце все штучки красиво визуальненько переместить)))))0)000)00)
        }

        private Cell PopCell(List<Cell> cells)
        {
            var cell = cells[Random.Range(0, cells.Count)];
            
            cells.Remove(cell);
            
            return cell;
        }
    }
}