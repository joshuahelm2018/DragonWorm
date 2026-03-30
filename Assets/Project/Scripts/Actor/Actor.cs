using UnityEngine;

namespace DragonWorm {
    [SelectionBase]
    public class Actor : Damageable {
        protected Transform _model;

        public override void Initialize() {
            base.Initialize();
            _model = transform.Find("Model");
        }

        protected virtual void Update() {
        
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {

        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            
        }
    }
}
