using UnityEngine;

namespace DragonWorm {
    public abstract class WeaponStrategy : ScriptableObject {
        [SerializeField] int damage = 10;
        [SerializeField] float attackRate = 0.5f;
        [SerializeField] protected float projectileSpeed = 10f;
        [SerializeField] protected float projectileLifetime = 4f;
        [SerializeField] protected GameObject projectilePrefab;

        public int Damage => damage;
        public float AttackRate => attackRate;

        public virtual void Initialize() {

        }

        public abstract void Attack(Transform firePoint, LayerMask layer);
    }
}
