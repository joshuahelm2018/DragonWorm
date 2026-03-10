using UnityEngine;

namespace DragonWorm {
    public class EnemyWeapon : Weapon {
        float attackTimer;

        private void Update() {
            attackTimer += Time.deltaTime;

            if (attackTimer >= weaponStrategy.AttackRate) {
                weaponStrategy.Attack(attackPoint, layer);
                attackTimer = 0f;
            }
        }
    }
}
