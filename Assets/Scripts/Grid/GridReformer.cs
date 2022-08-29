using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Grid
{
    /// <summary>
    /// Класс, Переставляющий предметы в ячейках различными
    /// способами
    /// </summary>
    public class GridReformer : MonoBehaviour
    {
        public static readonly UnityEvent OnReformedToNoPossibleMatches = new UnityEvent();
        /// <summary>
        /// Переставление предметов в ячейках по
        /// заданному шаблону для теста работы
        /// анализатора возможных 3+ в ряд. Метод
        /// Берет случайные значения, т.е. предыдущие
        /// предметы в сетке не учитываются
        /// </summary>
        public void ReformGridWithNoPossibleMatches()
        {
            var grid = GridContainer.Inst.Grid;
            var variants = GridContainer.Inst.Variants;
            var index = 0;

            for (var j = 0; j < GridContainer.Inst.Y; j++)
            {
                for (var i = 0; i < GridContainer.Inst.X; i++, index++)
                {
                    var cell = grid[i, j];
                    
                    if(cell.Empty) continue;

                    if (index >= variants.Count) index = 0;
                    Destroy(cell.Item.gameObject);
                    cell.InstantiateItem(variants[index]);
                }
            }
            OnReformedToNoPossibleMatches?.Invoke();
        }
        /// <summary>
        /// Реформирование элементов сетки в случае
        /// отсутствия возможных ходов. Все элементы
        /// с предыдущего расположения сохраняются и
        /// получают новую случайную позицию
        /// </summary>
        public void ReformGrid()
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
        /// <summary>
        /// Перестановка элементов рандомным образом
        /// в случае нахождения 3+ в ряд во время 
        /// реформирования сетки
        /// </summary>
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
        /// <summary>
        /// Взятие из списка рандомного элемента
        /// с его удалением из этого списка
        /// </summary>
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