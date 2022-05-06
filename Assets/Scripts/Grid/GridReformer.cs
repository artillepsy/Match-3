using System;
using System.Collections.Generic;
using Cells;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grid
{
    public class GridReformer : MonoBehaviour
    {
        public void ReformGridWithNoMatches()
        {
            
        }

        private void ReformGrid()
        {
            // вызывается, когда на поле не может быть матчей. ПЕРЕСТАВЛЯЕТ старые элементы
            var cells = GridContainer.Inst.GetFilledCells();

            foreach (var cell in GridContainer.Inst.Grid)
            {
                if(cell.Empty) continue;

                var cell2 = PopRandomCell(cells);

                var item = cell.Item; 
                
                cell.Item = cell2.Item;

                cell2.Item = item;
            }
            
            RemoveMatches();

            foreach (var cell in GridContainer.Inst.Grid)
            {
                cell.Item.MoveToCell(cell);
            }
        }

        private Cell PopRandomCell(List<Cell> cells)
        {
            var cell = cells[Random.Range(0, cells.Count)];
            
            cells.Remove(cell);
            
            return cell;
        }

        private void RemoveMatches()
        {
            while (true)
            {
                var cells = GridContainer.Inst.GetFilledCells();
                
                var cellsToReform = new List<Cell>();
                
                GridCheckHelper.CheckXMatches(ref cellsToReform);
                
                GridCheckHelper.CheckYMatches(ref cellsToReform);

                if (cellsToReform.Count == 0) break;

                foreach (var cell in cellsToReform)
                {
                    var cell2 = PopRandomCell(cells);

                    var item = cell.Item; 
                
                    cell.Item = cell2.Item;

                    cell2.Item = item;
                }
            } 
        }

        private void Start()
        {
            GridChecker.OnNoPossibleMatchesFound.AddListener(ReformGrid);
        }
    }
}