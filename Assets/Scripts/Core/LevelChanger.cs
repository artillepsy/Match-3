using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelChanger : MonoBehaviour
    {

        public void OnClickRestartLevel()
        {
            SceneManager.LoadSceneAsync("Game");
        }
        
        public void OnClickReformLevel()
        {
            
        }
    }
}