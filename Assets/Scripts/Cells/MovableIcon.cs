using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class MovableIcon : MonoBehaviour
    {
        public void SetColor(Color color)
        {
            var image = GetComponent<Image>();
            image.color = color;
        }
    }
}