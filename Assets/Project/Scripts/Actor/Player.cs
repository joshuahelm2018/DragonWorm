using UnityEngine;

namespace DragonWorm {
    public class Player : Actor {
        protected override void HandleDie() {
            GameManager.Instance.HandlePlayerDied();

            base.HandleDie();
        }
    }
}
