using UnityEngine;

namespace Cells
{
    /// <summary>
    /// Класс, хранящийся в GridContainer. В нём изменяется лишь информация о текущем тайле и хранится текущий ID
    /// </summary>
    public class Cell : MonoBehaviour
    {
        [SerializeField] private MovableIcon iconPrefab;
        
        private MovableIcon _icon;

        private CellVariant _variant;

        private bool _empty = false;

        private int _id;

        public bool Empty
        {
            get => _empty;
            set => _empty = value;
        }

        public int Id => _id;
        
        public void SetVariant(CellVariant newVariant)
        {
            _variant = newVariant;

            _id = newVariant.Id;
            
            _icon = Instantiate(iconPrefab, transform);
            
            _icon.SetSprite(newVariant.CellSprite);
            
            Debug.Log("New variant setted. Id: "+ newVariant.Id);
        }
    }
}