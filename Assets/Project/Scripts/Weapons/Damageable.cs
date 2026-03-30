using UnityEngine;

namespace DragonWorm {
    [RequireComponent(typeof(Life))]
    public class Damageable : MonoBehaviour {
        public Life Life { get; protected set; }
        protected BoxCollider2D _collider;
        protected Rigidbody2D _rb;

        protected virtual void Awake() {
            Initialize();
        }

        public virtual void Initialize() {
            Life = GetComponent<Life>();
            _collider = GetComponent<BoxCollider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable() {
            if (Life != null) {
                Life.OnDie += HandleDie;
            }
        }

        protected virtual void OnDisable() {
            if (Life != null) {
                Life.OnDie -= HandleDie;
            }
        }

        public virtual void TakeDamage(int amount) {
            if (Life) {
                Life.TakeDamage(amount);
            }
        }

        public virtual void TakeMaxDamage() {
            if (Life) {
                Life.TakeMaxDamage();
            }
        }

        protected virtual void HandleDie() {
            Destroy(gameObject);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            Collide(transform.position, collision: collision);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider) {
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
                DoCollision(hitObj, point, collision, collider);
            }
        }

        protected virtual void DoCollision(GameObject hitObj, Vector3 point, Collision2D? collision = null, Collider2D? collider = null) {

        }
    }
}
