using UnityEngine;

namespace DragonWorm {
    public class Actor : MonoBehaviour {
        public Life Life { get; protected set; }

        protected virtual void Awake() {
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
