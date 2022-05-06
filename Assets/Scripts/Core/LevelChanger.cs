using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelChanger : MonoBehaviour
    {
        private GridReformer _gridReformer;
        
        public void OnClickRestartLevel()
        {
            SceneManager.LoadSceneAsync("Game");
        }
        
        public void OnClickReformWithNoMatches()
        {
            _gridReformer.ReformGridWithNoMatches();
        }

        private void Awake()
        {
            _gridReformer = GetComponent<GridReformer>();
        }
    }
}