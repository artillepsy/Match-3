using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class CellSelectionBorder : MonoBehaviour
    {
        private Image _image;
        private bool _enabled = true;
        private void Start()
        {
            _image = GetComponent<Image>();

            _image.enabled = false;
            
            Cell.OnClickCell.AddListener(UpdatePosition);   
            
            InputListener.OnInputStatusChanged.AddListener((status) =>
            {
                _enabled = status;
                
                if (!status) _image.enabled = false;
            });
        }

        private void UpdatePosition(Cell cell)
        {
            if (_enabled) _image.enabled = true;
            
            transform.SetParent(cell.transform, false);
        }
    }
}