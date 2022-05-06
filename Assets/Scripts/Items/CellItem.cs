using System.Collections;
using Cells;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items
{
    public class CellItem : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        
        public static UnityEvent OnMoved = new UnityEvent();
        private Vector2 _translation;
        private Vector2 _endPosition;
        private int _id;
        
        public int Id => _id;
        
        public void SetVariant(ItemVariant variant)
        {
            var image = GetComponent<Image>();
            
            image.color = variant.ItemColor;

            _id = variant.Id;
        }

        public void MoveToCell(Cell cell)
        {
            _endPosition = cell.transform.position;
            
            transform.SetParent(cell.transform);

            StartCoroutine(MoveCO());
        }

        private IEnumerator MoveCO()
        {
            while (true)
            {
                
               var pos = Vector2.MoveTowards(
                   transform.position, 
                   _endPosition, 
                   speed * Time.deltaTime);

               if (pos == _endPosition)  break;

               transform.position = pos;

               yield return null;
            }
            OnMoved?.Invoke();
        }
    }
}