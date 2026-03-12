using System;
using UnityEngine;

namespace DragonWorm {
    public class Projectile : MonoBehaviour {
        [SerializeField] float speed;
        [SerializeField] int damage;
        [SerializeField] GameObject muzzlePrefab;
        [SerializeField] GameObject hitPrefab;

        Transform parent;

        public Action Callback;

        public void SetSpeed(float speed) {
            this.speed = speed;
        }

        public void SetParent(Transform parent) {
            this.parent = parent;
        }

        public void SetDamage(int damage) {
            this.damage = damage;
        }
        

        private void Start() {
            if (muzzlePrefab != null) {
                var muzzleVfx = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVfx.transform.forward = transform.forward;
                muzzleVfx.transform.SetParent(parent);

                StopVFX(muzzleVfx);
            }
        }

        private void Update() {
            transform.SetParent(null);
            transform.position += transform.right * (speed * Time.deltaTime);

            if (Callback != null) {
                Callback.Invoke();
            }
        }        

        private void OnCollisionEnter2D(Collision2D collision) {
            if (hitPrefab != null) {
                ContactPoint2D contact = collision.GetContact(0);
                var hitVfx = Instantiate(hitPrefab, contact.point, Quaternion.identity);

                StopVFX(hitVfx);

            }

            if (collision.gameObject.TryGetComponent(out Life victim)) {
                victim.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        private void StopVFX(GameObject vfx) {
            Animator anim = null;
            float length = 0f;
            if (!vfx.TryGetComponent(out anim)) {
                anim = vfx.GetComponentInChildren<Animator>();
                length = anim.GetCurrentAnimatorStateInfo(0).length;
            }

            Destroy(vfx, length);
        }
    }
}
