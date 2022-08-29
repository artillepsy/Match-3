using Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    /// <summary>
    /// Класс индикатора выбранной ячейки
    /// </summary>
    public class SelectionIndicator : MonoBehaviour
    {
        private Image _image;
        private bool _enabled = true;
        private void Start()
        {
            _image = GetComponent<Image>();
            _image.enabled = false;
            Cell.OnClickCell.AddListener(UpdatePosition);   
            
            // Деактивация индикатора, когда над полем произведено действие
            CellButtonsListener.OnInputStatusChanged.AddListener((status) =>
            {
                _enabled = status;
                if (!status) _image.enabled = false;
            });
        }
        /// <summary>
        /// Обновление позиции индикатора при нажатии на на ячейку
        /// </summary>
        private void UpdatePosition(Cell cell)
        {
            if (_enabled) _image.enabled = true;
            transform.SetParent(cell.transform, false);
        }
    }
}