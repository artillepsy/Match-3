using UnityEngine;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        private GridContainer _gridContainer;

        public bool NoMatches()
        {
            
        }

        private void Start() => _gridContainer = FindObjectOfType<GridContainer>();
    }
}