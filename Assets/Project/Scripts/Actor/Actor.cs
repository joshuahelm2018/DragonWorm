using UnityEngine;

namespace DragonWorm {
    [RequireComponent(typeof(Life))]
    public class Actor : MonoBehaviour {
        public Life Life { get; protected set; }

        protected virtual void Awake() {
            Initialize();
        }

        public virtual void Initialize() {
            Life = GetComponent<Life>();
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

        protected virtual void HandleDie() {
            Destroy(gameObject);
        }

        void Update() {
        
        }
    }
}
