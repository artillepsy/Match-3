using Cells;
using Grid;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Inputs
{
    public class CellButtonsListener : MonoBehaviour
    {
        public static UnityEvent<bool> OnInputStatusChanged = new UnityEvent<bool>();
        private Cell _lastCell;
        private Cell _newCell;
        private bool _shouldUndo = false;

        public bool ShouldUndo => _shouldUndo;

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener((cells) => _shouldUndo = false);
            GridChecker.OnNoMatchesFound.AddListener( () =>
            {
                if(_shouldUndo) UndoAction();
                else ClearSelection();
            });
            GridChecker.OnNoPossibleMatchesFound.AddListener((delay) =>
            {
                OnInputStatusChanged?.Invoke(false);
            });
            Cell.OnClickCell.AddListener(CheckSelection);
        }

        private void CheckSelection(Cell newCell)
        {
            if (!_lastCell || newCell.Item.Id == _lastCell.Item.Id)
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
            _newCell = newCell;
            ItemMover.Inst.SwapItems(_lastCell, _newCell);
            _shouldUndo = true;
            OnInputStatusChanged?.Invoke(false);
        }

        private void ClearSelection()
        {
            _shouldUndo = false;
            _newCell = null;
            _lastCell = null;
            OnInputStatusChanged?.Invoke(true);
        }

        private void UndoAction()
        {
            _shouldUndo = false;
            ItemMover.Inst.SwapItems(_lastCell, _newCell);
        }
    }
}