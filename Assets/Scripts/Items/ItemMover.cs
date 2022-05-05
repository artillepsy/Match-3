using Cells;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class ItemMover : MonoBehaviour
    {
        public static UnityEvent OnAllMoved = new UnityEvent();
        private int _remaining = 0;
        public static ItemMover Instance { get; private set; }

        public void SwapItems(Cell firstCell, Cell secondCell)
        {
            var buff = firstCell.Item;

            firstCell.Item.MoveToCell(secondCell);

            secondCell.Item.MoveToCell(firstCell);

            firstCell.Item = secondCell.Item;

            secondCell.Item = buff;

            _remaining = 2;
        }

        private void ReduceRemainingCount()
        {
            if (--_remaining == 0)
            {
                OnAllMoved?.Invoke();
                
                Debug.Log("remaining = 0");
            }
        }

        private void Awake() => Instance = this;

        private void Start()
        {
            CellItem.OnMoved.AddListener(ReduceRemainingCount);
        }
    }
}