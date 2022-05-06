using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inputs
{
    public class ButtonsListener : MonoBehaviour
    {
        private GridReformer _gridReformer;
        private bool _inputStatus = true;
        
        public void OnClickRestartLevel()
        {
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnClickReformWithNoMatches()
        {
            if (!_inputStatus) return;
            _gridReformer.ReformGridWithNoMatches();
        }

        private void Awake() => _gridReformer = GetComponent<GridReformer>();

        private void Start()
        {
            CellButtonsListener.OnInputStatusChanged.AddListener((status) => _inputStatus = status);
        }
    }
}