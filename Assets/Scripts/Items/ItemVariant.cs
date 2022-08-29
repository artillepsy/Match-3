using UnityEngine;

namespace Items
{
    /// <summary>
    /// Класс, хранящий идентификатор и цвет предмета
    /// </summary>
    [CreateAssetMenu(fileName = "Variant", menuName = "Variants/New variant", order = 0)]
    public class ItemVariant : ScriptableObject
    {
        public Color ItemColor;
        public int Id;
    }
}