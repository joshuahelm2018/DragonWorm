using UnityEngine;

namespace DragonWorm {
    public class Player : Actor {
        [SerializeField] private int smashDamage = 50;

        protected override void HandleDie() {
            GameManager.Instance.HandlePlayerDied();

            base.HandleDie();
        }

        protected override void OnCollisionEnter2D(Collision2D collision) {
            Collide(transform.position, collision: collision);
        }

        protected override void OnTriggerEnter2D(Collider2D collider) {
            Collide(transform.position, collider: collider);
        }

        private void Collide(Vector3 point, Collision2D? collision = null, Collider2D? collider = null) {
            GameObject hitObj = null;
            if (collision != null) {
                hitObj = collision.gameObject;
            } else if (collider != null) {
                hitObj = collider.gameObject;
            }

            if (hitObj != null) {
                if (hitObj.TryGetComponent(out ISmashable victim)) {
                    victim.SmashInto(smashDamage);
                }
            }
        }
    }
}
