using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class MovableIcon : MonoBehaviour
    {
        public void SetSprite(Sprite sprite)
        {
            var image = GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}