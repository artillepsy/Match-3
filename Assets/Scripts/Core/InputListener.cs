using Cells;
using Grid;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class InputListener : MonoBehaviour
    {
        public static UnityEvent<bool> OnInputSettingsChanged;

        private Cell _lastCell;
        private Cell _newCell;
        
        private void Awake() => OnInputSettingsChanged = new UnityEvent<bool>();

        private void Start()
        {
            ItemMover.OnAllMoved.AddListener((undo) =>
            {
                if (!undo) return;
                
                OnInputSettingsChanged?.Invoke(true);
            });
            // add after checking
            GridChecker.OnMatchesNotFound.AddListener(UndoAction);
            
            Cell.OnClickCell.AddListener(CheckSelection);
        }

        private void CheckSelection(Cell newCell)
        {
            if (!_lastCell)
            {
                _lastCell = newCell;
                return;
            }

            if (newCell.Item.Id == _lastCell.Item.Id)
            {
                _lastCell = newCell;
                return;
            }

            var x = Mathf.Abs(newCell.X - _lastCell.X);
            
            var y = Mathf.Abs(newCell.Y - _lastCell.Y);

            if (x == 1 && y == 1 || x > 1 || y > 1)
            {
                _lastCell = newCell;
                return;
            }

            Debug.Log("swap");

            _newCell = newCell;
            
            ItemMover.Inst.SwapItems(_lastCell, _newCell);

            OnInputSettingsChanged?.Invoke(false);
        }

        private void UndoAction()
        {
            Debug.Log("Undo");
            
            ItemMover.Inst.SwapItems(_lastCell, _newCell, true);

            _newCell = null;

            _lastCell = null;
        }
    }
}