using Cells;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    /// <summary>
    /// Класс, отвечающий за передвижения предметов.
    /// Вызывает у них методы передвижения и указывает
    /// конечные позиции
    /// </summary>
    public class ItemMover : MonoBehaviour
    {
        public static readonly UnityEvent OnAllMoved = new UnityEvent();
        private int _remaining = 0;
        public static ItemMover Inst { get; private set; }
        /// <summary>
        /// Задание двум предметам движения
        /// на соседнюю ячейку
        /// </summary>
        public void SwapItems(Cell firstCell, Cell secondCell)
        {
            var buff = firstCell.Item;
            firstCell.Item.MoveToCell(secondCell);
            secondCell.Item.MoveToCell(firstCell);
            firstCell.Item = secondCell.Item;
            secondCell.Item = buff;
            _remaining = 2;
        }
        /// <summary>
        /// Задание движения предмету от одной ячейки к другой
        /// </summary>
        public void MoveItem(Cell currentCell, Cell newCell)
        {
            var item = currentCell.Item;
            _remaining++;
            currentCell.Item = null;
            item.MoveToCell(newCell);
            newCell.Item = item;
        }
        /// <summary>
        /// Задание движения ячейки с указанной ячейке
        /// </summary>
        public void MoveItem(Cell cell)
        {
            _remaining++;
            cell.Item.MoveToCell(cell);
        }
        /// <summary>
        /// Уменьшение количества передвигающихся клеток.
        /// После того, как каждая клетка достигла
        /// места назначения, счетчик уменьшается.
        /// После остановки всех клеток вызывается
        /// ивент
        /// </summary>
        private void ReduceRemainingCount()
        {
            if (--_remaining != 0) return;
            OnAllMoved?.Invoke();
        }

        private void Awake() => Inst = this;

        private void Start() => CellItem.OnMoved.AddListener(ReduceRemainingCount);

        private void OnDestroy() => CellItem.OnMoved.RemoveListener(ReduceRemainingCount);
    }
}