using UnityEngine;
using Utilities;

namespace DragonWorm {
    [CreateAssetMenu(fileName = "TrackingShot", menuName = "DragonWorm/WeaponStrategy/TrackingShot", order = 0)]
    public class TrackingShot : WeaponStrategy {
        [SerializeField] float trackingSpeed = 5f;

        Transform target;

        public override void Initialize() {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) {
                target = p.transform;
            }
        }

        public override void Attack(Transform attackPoint, LayerMask layer) {
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
            projectile.transform.SetParent(attackPoint);
            projectile.layer = layer;

            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetDamage(Damage);
            projectileComponent.SetSpeed(projectileSpeed);
            projectile.GetComponent<Projectile>().Callback = () => {
                Vector3 currentDir = projectile.transform.right;
                Vector3 targetDir = projectile.transform.right;
                if (target != null) {
                    targetDir = (target.position - projectile.transform.position).normalized;
                }

                Vector3 newDir = Vector3.RotateTowards(
                    currentDir,
                    targetDir,
                    trackingSpeed * Mathf.Deg2Rad * Time.deltaTime,
                    0f
                );

                projectile.transform.right = newDir;
            };

            if (projectileLifetime > 0) {
                Destroy(projectile, projectileLifetime);
            }
        }
    }
}
