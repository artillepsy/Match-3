using Cells;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class ItemMover : MonoBehaviour
    {
        public static UnityEvent OnAllMoved = new UnityEvent();
        private int _remaining = 0;
        public static ItemMover Inst { get; private set; }

        public void SwapItems(Cell firstCell, Cell secondCell)
        {
            var buff = firstCell.Item;

            firstCell.Item.MoveToCell(secondCell);

            secondCell.Item.MoveToCell(firstCell);

            firstCell.Item = secondCell.Item;

            secondCell.Item = buff;
            
            _remaining = 2;
        }

        public void MoveItem(Cell currentCell, Cell newCell)
        {
            var item = currentCell.Item;
            
            _remaining++;

            currentCell.Item = null;
            
            item.MoveToCell(newCell);

            newCell.Item = item;
        }

        public void MoveItem(Cell cell)
        {
            _remaining++;
            
            cell.Item.MoveToCell(cell);
        }
        
        private void ReduceRemainingCount()
        {
            if (--_remaining != 0) return;
            
            OnAllMoved?.Invoke();
        }

        private void Awake() => Inst = this;

        private void Start()
        {
            CellItem.OnMoved.AddListener(ReduceRemainingCount);
        }
    }
}