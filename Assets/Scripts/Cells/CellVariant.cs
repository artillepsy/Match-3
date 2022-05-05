using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(fileName = "Variant", menuName = "Variants/New variant", order = 0)]
    public class CellVariant : ScriptableObject
    {
        //public Sprite CellSprite;

        public Color CellColor;

        public int Id;
    }
}