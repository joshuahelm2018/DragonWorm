using UnityEngine;

namespace DragonWorm {
    public class BuildingPart : MonoBehaviour {
        BoxCollider2D _collider;
        Rigidbody2D _rb;

        void Start() {
            _collider = GetComponent<BoxCollider2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void EnablePhysics() {
            _collider.enabled = true;
            _rb.simulated = true;
        }
    }
}
