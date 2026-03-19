using DragonWorm.Input;
using UnityEngine;

namespace DragonWorm {
    public class PlayerWeapon : Weapon {
        InputReader input;
        float attackTimer;

        private void Awake() {
            input = GetComponent<InputReader>();
        }

        private void Update() {
            attackTimer += Time.deltaTime;

            if (input.Attack && attackTimer >= weaponStrategy.AttackRate) {
                weaponStrategy.Attack(attackPoint, layer);
                attackTimer = 0f;
            }
        }
    }
}
