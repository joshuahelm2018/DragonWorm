using UnityEngine;

namespace DragonWorm {
    public class Player : Actor {
        [SerializeField] private int collideDamage = 50;

        protected override void HandleDie() {
            GameManager.Instance.HandlePlayerDied();

            base.HandleDie();
        }

        protected override void DoCollision(GameObject hitObj, Vector3 point, Collision2D? collision = null, Collider2D? collider = null) {
            if (hitObj != null) {
                if (hitObj.TryGetComponent(out Edible food)) {
                    Eat(food);
                } else if (hitObj.TryGetComponent(out Damageable victim)) {
                    victim.TakeDamage(collideDamage);
                }
            }
        }

        protected void Eat(Edible food) {
            food.EatenBy(this);
        }
    }
}
