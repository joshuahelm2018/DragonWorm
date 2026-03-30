using UnityEngine;

namespace DragonWorm {
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodySegment : MonoBehaviour {
        Rigidbody2D rb;
        Life life;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        public void Initialize(Life life) {
            this.life = life;
        }

        public void MoveTo(Vector3 pos, Quaternion rot) {
            rb.MovePosition(pos);
            rb.MoveRotation(rot);
            //rb.MovePositionAndRotation(pos, rot);
        }

        public void TakeDamage(int amount) {
            if (life) {
                life.TakeDamage(amount);
            }
        }
    }
}
