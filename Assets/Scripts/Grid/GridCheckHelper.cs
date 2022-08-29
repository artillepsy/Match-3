using System.Collections.Generic;
using Cells;

namespace Grid
{
    /// <summary>
    /// Класс, содержащий методы для проверки сетки с ячейками
    /// на три+ в ряд или возможность их собрать
    /// </summary>
    public static class GridCheckHelper
    {
        /// <summary>
        /// Массив позиций для диагональной проверки возможных три+ в ряд.
        /// Два верхних столбца - позиции Х и У ячеек для проверки на одинаковость.
        /// Два нижних столбца - позиции Х и У ячеек для проверки на пустоту.
        /// </summary>
        private static readonly int[,] _diagonalCheckArray = new int[4, 5]
        {
            { -1,  1, 1, -1, -1}, 
            { -1, -1, 1,  1, -1},
            {  0,  1, 1,  0, -1}, // Х пустышек
            { -1,  0, 1,  1,  0} // У пустышек
        };
        /// <summary>
        /// Массив позиций для крестовой проверки возможных три+ в ряд.
        /// Два верхних столбца - позиции Х и У ячеек для проверки на одинаковость.
        /// Два нижних столбца - позиции Х и У ячеек для проверки на пустоту.
        /// Последняя нижняя позиция не используется
        /// </summary>
        private static readonly int[,] _crossCheckArray = new int[4, 9]
        {
            {-1, 0, 1,-1, 1, 0,-1, 1,-1},
            { 1,-1, 1, 0,-1, 1,-1, 0, 1},
            { 0, 0, 1, 1, 0, 0,-1,-1, 0}, // Х пустышек. последний не испьзуется
            {-1,-1, 0, 0, 1, 1, 0, 0, 0}  // У пустышек
        };
        /// <summary>
        /// Массив позиций для дальней проверки возможных три+ в ряд.
        /// Два верхних столбца - позиции Х и У ячеек для проверки на одинаковость.
        /// Два нижних столбца - позиции Х и У ячеек для проверки на пустоту.
        /// </summary>
        private static readonly int[,] _farCheckArray = new int[4, 8]
        {
            { 0, 0, 2, 3, 0, 0,-2,-3},
            {-2,-3, 0, 0, 2, 3, 0, 0},
            { 0, 0, 1, 1, 0, 0,-1,-1}, // Х пустышек
            {-1,-1, 0, 0, 1, 1, 0, 0}  // У пустышек
        };
        /// <summary>
        /// Метод для горизонтальной проверки на три+ в ряд.
        /// Заносит в случае нахождения ячейки в список
        /// </summary>
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
                    var needCheck = false;

                    if (grid[i, j].Empty) 
                    {
                        id = -1;
                        needCheck = true;
                    }
                    else if (id != grid[i, j].Item.Id)
                    {
                        id = grid[i, j].Item.Id;
                        needCheck = true;
                    }
                    else counter++;

                    if (!needCheck && i != x - 1) continue;
                    
                    if (counter < 3)
                    {
                        counter = 1; 
                        continue;
                    }
                    
                    for (var k = (i == x - 1 && !needCheck) ? i : (i - 1); counter > 0; k--, counter--)
                    {
                        if (cellBuff.Contains(grid[k, j])) continue;
                        cellBuff.Add(grid[k, j]);
                    } 
                    counter = 1;
                }
            }
        }
        /// <summary>
        /// Метод для вертикальной проверки на три+ в ряд.
        /// Заносит в случае нахождения ячейки в список
        /// </summary>
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
                    var needCheck = false;

                    if (grid[i, j].Empty)
                    {
                        id = -1;
                        needCheck = true;
                    }
                    else if (id != grid[i, j].Item.Id)
                    {
                        id = grid[i, j].Item.Id;
                        needCheck = true;
                    }
                    else counter++;

                    if (!needCheck && j != y - 1) continue;
                    
                    if (counter < 3)
                    {
                        counter = 1;
                        continue;
                    }
                    for (var k = (j == y - 1 && !needCheck) ? j : (j - 1); counter > 0; k--, counter--)
                    {
                        if (cellBuff.Contains(grid[i, k])) continue;
                        cellBuff.Add(grid[i, k]);
                    } 
                    counter = 1;
                }
            }
        }
        /// <summary>
        /// Проверка на то, можно ли собрать три+ в ряд
        /// </summary>
        public static bool FindPossibleMatches()
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            
            for (var j = 0; j < y; j++)
            {
                for (var i = 0; i < x; i++)
                { 
                    if (CanMatch(i, j, _diagonalCheckArray)) return true;
                    if (CanMatch(i, j, _crossCheckArray)) return true;
                    if (CanMatch(i, j, _farCheckArray)) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Проверка соседних ячеек на комбинацию, которая
        /// может дать три+ в ряд. Здесь также проверяется,
        /// не мешают ли комбинации пустые ячейки
        /// </summary>
        private static bool CanMatch(int i, int j, int[,] arr)
        {
            var grid = GridContainer.Inst.Grid;
            if (grid[i, j].Empty) return false;
            var id = grid[i, j].Item.Id;

            for (var k = 0; k < arr.GetLength(1) - 1; k++)
            {
                if (!TryGetCell(i + arr[0, k], j + arr[1, k], out var cell1)) continue;
                if (!TryGetCell(i + arr[0, k + 1], j + arr[1, k + 1], out var cell2)) continue;
                if (grid[i + arr[2, k], j + arr[3, k]].Empty) continue; // проверка на пустышку в месте "стыковки"
                if (cell1.Empty || cell2.Empty) continue;

                if (cell1.Item.Id == id && cell2.Item.Id == id) return true;
            }
            return false;
        }
        /// <summary>
        /// Попытка найти ячейку по заданным i и j.
        /// В случае нахождения значение записывается в
        /// выходной параметр 
        /// </summary>
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