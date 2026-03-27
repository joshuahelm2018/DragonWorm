using UnityEngine;

namespace DragonWorm {
    public class Building : Actor, ISmashable {
        protected BoxCollider2D _collider;
        protected Rigidbody2D _rb;
        protected BuildingPart[] parts;

        public override void Initialize() {
            base.Initialize();

            _collider = GetComponent<BoxCollider2D>();
            _rb = GetComponent<Rigidbody2D>();

            if (_model) {
                parts = _model.GetComponentsInChildren<BuildingPart>();
            }
        }

        protected override void HandleDie() {
            Destroy(_rb);
            _collider.enabled = false;

            foreach (BuildingPart part in parts) {
                part.EnablePhysics();
            }
        }

        public void SmashInto(int damageAmount) {
            Life.TakeDamage(damageAmount);
        }
    }
}
