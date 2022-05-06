using System.Collections.Generic;
using Cells;

namespace Grid
{
    public static class GridCheckHelper
    {
        private static readonly int[,] DiagonalCheckArray = 
        {
            { -1,  1, 1, -1, -1},
            { -1, -1, 1,  1, -1}
        };

        private static readonly int[,] CrossCheckArray = 
        {
            {-1, 0, 1,-1, 1, 0,-1, 1,-1},
            { 1,-1, 1, 0,-1, 1,-1, 0, 1}
        };
        
        public static bool FindPossibleMatches()
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            
            for (var j = 1; j < y - 1; j++)
            {
                for (var i = 1; i < x - 1; i++)
                {
                    if (CanMatch(i, j, DiagonalCheckArray)) return true;
                    
                    if (CanMatch(i, j, CrossCheckArray)) return true;
                }
            }
            return false;
        }
        
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

        private static bool CanMatch(int i, int j, int[,] arr)
        {
            var grid = GridContainer.Inst.Grid;

            if (grid[i, j].Empty) return false;

            var id = grid[i, j].Item.Id;

            for (var k = 0; k < arr.Length-1; k++)
            {
                var cell1 = grid[i + arr[0, k], j + arr[1, k]];
                
                var cell2 = grid[i + arr[0, k+1], j + arr[1, k+1]];
                
                if (cell1.Empty || cell2.Empty) continue;

                if (cell1.Item.Id == id && cell2.Item.Id == id) return true;
            }
            return false;
        }
    }
}