using UnityEngine;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        public GridChecker Inst { get; private set; }
        
        

        // после свапа проверить на наличие матчей. Если их нет - вернуть false
        // после успешной проверки вызвать апдейт grid'a. После апдейта снова проверить,
        // но сначала на матчи, а потом на то, могут ли они быть на сцене

        private void Awake() => Inst = this;
    }
}