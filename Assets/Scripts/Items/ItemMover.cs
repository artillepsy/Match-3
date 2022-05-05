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

            currentCell.Item = null;
            
            item.MoveToCell(newCell);

            newCell.Item = item;

            _remaining++;
        }

        public void MoveItem(Cell cell, Vector2 startPos)
        {
            cell.Item.transform.position = startPos;
            
            cell.Item.MoveToCell(cell);

            _remaining++;
        }
        
        private void ReduceRemainingCount()
        {
            if (--_remaining != 0) return;

            Debug.Log(_remaining);
            
            OnAllMoved?.Invoke();
        }

        private void Awake() => Inst = this;

        private void Start()
        {
            CellItem.OnMoved.AddListener(ReduceRemainingCount);
        }
    }
}