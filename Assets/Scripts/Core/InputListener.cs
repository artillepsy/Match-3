using Cells;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class InputListener : MonoBehaviour
    {
        public static UnityEvent<bool> OnInputSettingsChanged;

        // private bool _inputEnabled = true;
        private Cell _lastCell;
        private Cell _newCell;
        
        private void Awake() => OnInputSettingsChanged = new UnityEvent<bool>();

        private void Start() => Cell.OnClickCell.AddListener(CheckSelection);

        private void CheckSelection(Cell newCell)
        {
            if (newCell.Id == _lastCell.Id) return;

            var x = Mathf.Abs(newCell.X - _lastCell.X);
            
            var y = Mathf.Abs(newCell.Y - _lastCell.Y);

            if (x == 1 && y == 1 || x > 1 || y > 1) return;
            
            // swap

        }
    }
}