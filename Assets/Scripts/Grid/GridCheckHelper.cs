using System.Collections.Generic;
using Cells;

namespace Grid
{
    public static class GridCheckHelper
    {
        public static void CheckXMatches(List<Cell> cellsToClear)
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
                    for (var k = i - 1; counter > 0; k--, counter--)
                    {
                        if (cellsToClear.Contains(grid[k, j])) continue;
                            
                        cellsToClear.Add(grid[k, j]);
                    } 
                    counter = 1;
                }
            }
        }
        
        public static void CheckYMatches(List<Cell> cellsToClear)
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
                    for (var k = j - 1; counter > 0; k--, counter--)
                    {
                        if (cellsToClear.Contains(grid[i, k])) continue;
                            
                        cellsToClear.Add(grid[i, k]);
                    } 
                    counter = 1;
                }
            }
        }
        
        public static bool CanMatchDiagonals(int i, int j)
        {
            var grid = GridContainer.Inst.Grid;

            if (grid[i, j].Empty) return false;

            var id = grid[i, j].Item.Id;

            if (!grid[i - 1, j - 1].Empty) // LU
            {
                if (!grid[i + 1, j - 1].Empty) // RU
                {
                    if (grid[i - 1, j - 1].Item.Id == grid[i + 1, j - 1].Item.Id) return true; //LU ? RU
                }
                if (!grid[i - 1, j + 1].Empty) // LD
                {
                    if (grid[i - 1, j - 1].Item.Id == grid[i - 1, j + 1].Item.Id) return true; // LU ? LD
                }
            }
            if (!grid[i + 1, j + 1].Empty) // RD
            {
                if (!grid[i + 1, j - 1].Empty) // RU
                {
                    if (grid[i + 1, j + 1].Item.Id == grid[i + 1, j - 1].Item.Id) return true; // RD ? RU
                }
                if (!grid[i - 1, j + 1].Empty) // LD
                {
                    if (grid[i + 1, j + 1].Item.Id == grid[i - 1, j + 1].Item.Id) return true; // RD ? LD
                }
            }

            return false;
        }
    }
}