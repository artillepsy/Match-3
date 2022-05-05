using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        public static readonly UnityEvent<List<Cell>> OnFoundMatches = new UnityEvent<List<Cell>>();
        public static readonly UnityEvent OnNoMatchesFound = new UnityEvent();
        public GridChecker Inst { get; private set; }

        public void CheckForMatches()
        {
            var cellsToClear = new List<Cell>();

            var x = GridContainer.Inst.X;
            
            var y = GridContainer.Inst.Y;

            CheckXMatches(ref cellsToClear, x, y);
            
            CheckYMatches(ref cellsToClear, x, y);

            if (cellsToClear.Count > 0)
            {
                OnFoundMatches?.Invoke(cellsToClear);
            }
            else OnNoMatchesFound?.Invoke();
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
            ItemMover.OnAllMoved.AddListener(CheckForMatches);
        }

        private void CheckXMatches(ref List<Cell> cellsToClear, int x, int y)
        {
            var grid = GridContainer.Inst.Grid;
            
            for (var j = 0; j < y; j++)
            {
                var counter = 1;
                var id = -1;

                for (var i = 0; i < x; i++)
                {
                    var startCheck = false;

                    if (grid[i, j].Empty)
                    {
                        id = -1;
                        startCheck = true;
                    }
                    else if (id != grid[i, j].Item.Id)
                    {
                        id = grid[i, j].Item.Id;
                        startCheck = true;
                    }
                    else counter++;
                    
                    if (!startCheck || i != x-1) continue;

                    if (counter < 3)
                    {
                        counter = 1;
                        if (id == -1) i++;
                        continue;
                    }
                    
                    for (var k = i - 1; counter > 0; k--, counter--)
                    {
                        if (cellsToClear.Contains(grid[k, j])) continue;
                            
                        cellsToClear.Add(grid[k, j]);
                    } 
                    
                    counter = 1;
                    if (id == -1) i++;
                }
            }
        }
        
        private void CheckYMatches(ref List<Cell> cellsToClear, int x, int y)
        {
            var grid = GridContainer.Inst.Grid;
            
            for (var i = 0; i < x; i++)
            {
                var counter = 1;
                var id = -1;

                for (var j = 0; j < y; j++)
                {
                    var startCheck = false;

                    if (grid[i, j].Empty)
                    {
                        id = -1;
                        startCheck = true;
                    }
                    else if (id != grid[i, j].Item.Id)
                    {
                        id = grid[i, j].Item.Id;
                        startCheck = true;
                    }
                    else counter++;
                    
                    if (!startCheck || j != y-1) continue;

                    if (counter < 3)
                    {
                        counter = 1;
                        if (id == -1) i++;
                        continue;
                    }
                    
                    for (var k = j - 1; counter > 0; k--, counter--)
                    {
                        if (cellsToClear.Contains(grid[i, k])) continue;
                            
                        cellsToClear.Add(grid[i, k]);
                    } 
                    
                    counter = 1;
                    if (id == -1) i++;
                }
            }
        }
    }
}