using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class SelectionBorder : MonoBehaviour
    {
        private Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();

            _image.enabled = false;
            
            Cell.OnClickCell.AddListener(UpdatePosition);   
            
            InputListener.OnInputSettingsChanged.AddListener((status) =>
            {
                if (!status) _image.enabled = false;
            });
        }

        private void UpdatePosition(Cell cell)
        {
            if (!_image.enabled) _image.enabled = true;
            
            transform.SetParent(cell.transform, false);
        }
    }
}