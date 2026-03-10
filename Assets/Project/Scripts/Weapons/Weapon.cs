using UnityEngine;
using Utilities;

namespace DragonWorm {
    public abstract class Weapon : MonoBehaviour {
        [SerializeField] protected WeaponStrategy weaponStrategy;
        [SerializeField] protected Transform attackPoint;
        [SerializeField, Layer] protected int layer;

        private void OnValidate() {
            layer = gameObject.layer;
        }

        private void Start() {
            weaponStrategy.Initialize();
        }

        public void SetWeaponStrategy(WeaponStrategy strategy) {
            weaponStrategy = strategy;
            weaponStrategy.Initialize();
        }
    }
}
