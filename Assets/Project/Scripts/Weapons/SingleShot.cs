using UnityEngine;

namespace DragonWorm {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "DragonWorm/WeaponStrategy/SingleShot", order = 0)]
    public class SingleShot : WeaponStrategy {
        public override void Attack(Transform firePoint, LayerMask layer) {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;

            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);
            projectileComponent.SetDamage(Damage);

            if (projectileLifetime > 0) {
                Destroy(projectile, projectileLifetime);
            }
        }
    }
}
