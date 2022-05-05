using System;
using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        public static UnityEvent<List<Cell>> OnFoundMatches = new UnityEvent<List<Cell>>();
        public static UnityEvent OnMathesNotFound = new UnityEvent();
        public GridChecker Inst { get; private set; }

        public void CheckForMatches()
        {
            var grid = GridContainer.Inst.Grid;

            var cellsToRemove = new List<Cell>();

            for (var j = 0; j < grid.GetLength(1); j++)
            {
                var counterX = 0;
                
                for (var i = 1; i < grid.GetLength(0); i++)
                {
                    if (grid[i - 1, j].Item.Id == grid[i, j].Item.Id) counterX++;
                    else if (counterX < 3) counterX = 0;
                    else
                    {
                        for (var k = i - 1; counterX >= 0; k--, counterX--)
                        {
                            if(cellsToRemove.Contains(grid[k, j])) continue;
                            
                            cellsToRemove.Add(grid[k, j]);
                        }
                        counterX = 0;
                    }
                }
            }
            
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                var counterY = 0;
                
                for (var j = 1; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j-1].Item.Id == grid[i, j].Item.Id) counterY++;
                    else if (counterY < 3) counterY = 0;
                    else
                    {
                        for (var k = j - 1; counterY >= 0; k--, counterY--)
                        {
                            if(cellsToRemove.Contains(grid[i, k])) continue;
                            
                            cellsToRemove.Add(grid[i, k]);
                        }
                        counterY = 0;
                    }
                }
            }
            if(cellsToRemove.Count > 0) OnFoundMatches?.Invoke(cellsToRemove);
            else OnMathesNotFound?.Invoke();
        }

        public void /*bool*/ FindPossibleMatches()
        {
            
        }

        // после свапа проверить на наличие матчей. Если их нет - вернуть false
        // после успешной проверки вызвать апдейт grid'a. После апдейта снова проверить,
        // но сначала на матчи, а потом на то, могут ли они быть на сцене

        private void Awake() => Inst = this;

        private void Start()
        {
            ItemMover.OnAllMoved.AddListener((undo) =>
            {
                if (undo) return;

                CheckForMatches();
            });
        }
    }
}