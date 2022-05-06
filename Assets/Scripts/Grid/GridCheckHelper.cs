using System.Collections.Generic;
using Cells;
using UnityEngine;

namespace Grid
{
    public static class GridCheckHelper
    {
        private static readonly int[,] _diagonalCheckArray = new int[2, 5]
        {
            { -1,  1, 1, -1, -1},
            { -1, -1, 1,  1, -1}
        };

        private static readonly int[,] _crossCheckArray = new int[2, 9]
        {
            {-1, 0, 1,-1, 1, 0,-1, 1,-1},
            { 1,-1, 1, 0,-1, 1,-1, 0, 1}
        };
        
        private static readonly int[,] _farCheckArray = new int[2, 8]
        {
            { 0, 0, 2, 3, 0, 0,-2,-3},
            {-2,-3, 0, 0, 2, 3, 0, 0}
        };
        
        public static void CheckXMatches(ref List<Cell> cellBuff)
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
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

                    if (!startCheck && i != x - 1) continue;
                    
                    if (counter < 3)
                    {
                        counter = 1; 

                        continue;
                    }
                    for (var k = (i == x - 1 && !startCheck) ? i : (i - 1); counter > 0; k--, counter--)
                    {
                        if (cellBuff.Contains(grid[k, j])) continue;
                            
                        cellBuff.Add(grid[k, j]);
                    } 
                    counter = 1;
                }
            }
        }
        
        public static void CheckYMatches(ref List<Cell> cellBuff)
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
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

                    if (!startCheck && j != y - 1) continue;
                    
                    if (counter < 3)
                    {
                        counter = 1;
                        
                        continue;
                    }
                    for (var k = (j == y - 1 && !startCheck) ? j : (j - 1); counter > 0; k--, counter--)
                    {
                        if (cellBuff.Contains(grid[i, k])) continue;
                            
                        cellBuff.Add(grid[i, k]);
                    } 
                    counter = 1;
                }
            }
        }
        
        public static bool FindPossibleMatches()
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            
            for (var j = 0; j < y; j++)
            {
                for (var i = 0; i < x; i++)
                {
                    if (CanMatch(i, j, _diagonalCheckArray))
                    {
                        Debug.Log("("+i+", "+ j +") diagonal");
                        return true;
                    }

                    if (CanMatch(i, j, _crossCheckArray))
                    {
                        Debug.Log("("+i+", "+ j +") cross");
                        return true;
                    }

                    if (CanMatch(i, j, _farCheckArray))
                    {
                        Debug.Log("("+i+", "+ j +") far");
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool CanMatch(int i, int j, int[,] arr)
        {
            var grid = GridContainer.Inst.Grid;
            
            if (grid[i, j].Empty) return false;

            var id = grid[i, j].Item.Id;

            for (var k = 0; k < arr.GetLength(0) - 1; k++)
            {
                if (!TryGetCell(i + arr[0, k], j + arr[1, k], out var cell1)) continue;
                
                if (!TryGetCell(i + arr[0, k + 1], j + arr[1, k + 1], out var cell2)) continue;
                
                if (cell1.Empty || cell2.Empty) continue;

                if (cell1.Item.Id == id && cell2.Item.Id == id)
                {
                    Debug.Log(k);
                    Debug.Log("("+arr[0, k]+", "+ arr[1, k] +") k");
                    Debug.Log("("+arr[0, k+1]+", "+ arr[1, k+1] +") k+1");
                    Debug.Log("("+cell1.X+", "+ cell1.Y +") cell1");
                    Debug.Log("("+cell2.X+", "+ cell2.Y +") cell2");
                    return true;
                }
            }
            return false;
        }

        private static bool TryGetCell(int i, int j, out Cell cell)
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;

            cell = null;

            if (i < 0 || j < 0 || i >= x || j >= y) return false;

            cell = GridContainer.Inst.Grid[i, j];

            return true;
        }
    }
}