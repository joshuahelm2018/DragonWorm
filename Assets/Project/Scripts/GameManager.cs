using Eflatun.SceneReference;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace DragonWorm
{
    public class GameManager : SingletonMono<GameManager> {
        [SerializeField] SceneReference mainMenuScene;
        [SerializeField] SceneReference gameOverScene;
        [SerializeField] float gameOverDuration = 3f;

        Player player;

        public bool IsGameOver() {
            return !player.Life.IsAlive;
        }

        protected override void Awake() {
            base.Awake();

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void HandlePlayerDied() {
            DoGameOver();
        }

        void DoGameOver() {
            Loader.Load(gameOverScene, LoadSceneMode.Additive);
            StartCoroutine(SwitchToMainMenu());
        }

        IEnumerator SwitchToMainMenu() {
            yield return new WaitForSeconds(gameOverDuration);
            Loader.Load(mainMenuScene);
        }
    }
}
