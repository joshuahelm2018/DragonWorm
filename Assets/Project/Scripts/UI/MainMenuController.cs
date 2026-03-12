using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace DragonWorm {
    public class MainMenuController : MonoBehaviour {
        private UIDocument _uiDocument;
        [SerializeField] private SceneReference startingLevel;

        VisualElement root;
        Button startBtn;
        Button quitBtn;

        private void Awake() {
            _uiDocument = GetComponent<UIDocument>();
            root = _uiDocument.rootVisualElement;
            startBtn = root.Q<Button>("Start");
            quitBtn = root.Q<Button>("Quit");
        }

        private void OnEnable() {
            if (startBtn != null) {
                startBtn.clicked += StartGame;
            }

            if (startBtn != null) {
                quitBtn.clicked += QuitApp;
            }
        }

        private void OnDisable() {
            if (startBtn != null) {
                startBtn.clicked -= StartGame;
            }

            if (startBtn != null) {
                quitBtn.clicked -= QuitApp;
            }
        }

        private void StartGame() {
            Loader.Load(startingLevel);
        }

        private void QuitApp() {
            Helpers.QuitGame();
        }
    }
}
