using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Grid
{
    public class GridReformer : MonoBehaviour
    {
        public static readonly UnityEvent OnReformedToNoPossibleMatches = new UnityEvent();
        public void ReformGridWithNoMatches()
        {
            var grid = GridContainer.Inst.Grid;
            var variants = GridContainer.Inst.Variants;
            var index = Random.Range(0, variants.Count);

            for (var j = 0; j < GridContainer.Inst.Y; j++)
            {
                for (var i = 0; i < GridContainer.Inst.X; i++, index++)
                {
                    var cell = grid[i, j];
                    
                    if(cell.Empty) continue;

                    if (index >= variants.Count) index = 0;
                    
                    Destroy(cell.Item.gameObject);
                    
                    cell.SetVariant(variants[index]);
                }
            }
            OnReformedToNoPossibleMatches?.Invoke();
        }
        
        

        private void ReformGrid()
        {
            while(true)
            {
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

                if (GridCheckHelper.FindPossibleMatches()) break;
            }  

            foreach (var cell in GridContainer.Inst.Grid)
            {
                if(cell.Empty) continue;
                
                ItemMover.Inst.MoveItem(cell);
            }
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
        
        private Cell PopRandomCell(List<Cell> cells)
        {
            var cell = cells[Random.Range(0, cells.Count)];
            
            cells.Remove(cell);
            
            return cell;
        }

        private void Start()
        {
            GridChecker.OnNoPossibleMatchesFound.AddListener((delay)=>
            {
                Invoke(nameof(ReformGrid), delay);
            });
        }
    }
}