using UnityEngine;

namespace DragonWorm {
    [SelectionBase]
    [RequireComponent(typeof(Life))]
    public class Actor : MonoBehaviour, IDamageable {
        protected Transform _model;

        public Life Life { get; protected set; }

        protected virtual void Awake() {
            Initialize();
        }

        public virtual void Initialize() {
            Life = GetComponent<Life>();
            _model = transform.Find("Model");
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

        public void TakeDamage(int amount) {
            if (Life) {
                Life.TakeDamage(amount);
            }
        }

        protected virtual void HandleDie() {
            Destroy(gameObject);
        }

        protected virtual void Update() {
        
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {

        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            
        }
    }
}
