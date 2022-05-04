using UnityEngine;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        private GridContainer _gridContainer;

        /*public List<Cell> Matches()
        {
            var cells = new List<Cell>();
            
            
        }*/

        private void Start() => _gridContainer = FindObjectOfType<GridContainer>();
    }
}