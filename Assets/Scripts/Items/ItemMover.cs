using Cells;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class ItemMover : MonoBehaviour
    {
        public static UnityEvent<bool> OnAllMoved = new UnityEvent<bool>();
        private int _remaining = 0;
        private bool _undo = false;
        public static ItemMover Inst { get; private set; }

        public void SwapItems(Cell firstCell, Cell secondCell, bool undo = false)
        {
            var buff = firstCell.Item;

            firstCell.Item.MoveToCell(secondCell);

            secondCell.Item.MoveToCell(firstCell);

            firstCell.Item = secondCell.Item;

            secondCell.Item = buff;

            _undo = undo;
            
            _remaining = 2;
        }

        public void MoveItem(Cell currentCell, Cell newCell)
        {
            var item = currentCell.Item;

            currentCell.Item = null;
            
            item.MoveToCell(newCell);
        }

        public void MoveItem(CellItem item, Cell newCell) => item.MoveToCell(newCell);

        public void SetRemaining(int newRemaining) => _remaining = newRemaining;

        private void ReduceRemainingCount()
        {
            if (--_remaining != 0) return;
            
            OnAllMoved?.Invoke(_undo);
                
            // Debug.Log("remaining = 0");
        }

        private void Awake() => Inst = this;

        private void Start()
        {
            CellItem.OnMoved.AddListener(ReduceRemainingCount);
        }
    }
}