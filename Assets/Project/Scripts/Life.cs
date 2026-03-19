using System;
using UnityEngine;

namespace DragonWorm {
    public class Life : MonoBehaviour {
        [SerializeField] int maxHealth = 10;

        [field: SerializeField]
        public int Health { get; protected set; }
        public int MaxHealth => maxHealth;

        [SerializeField] bool invincible = false;

        public bool IsAlive => Health > 0;

        public event Action OnDie;

        public Life(int amount) {
            maxHealth = amount;
            Initialize();
        }

        void Awake() {
            Initialize();
        }

        public void Initialize() {
            Health = maxHealth;
        }

        public void SetHealth(int amount) {
            Health = amount;
        }

        public void TakeDamage(int amount) {
            if (invincible) { return; }

            Health -= amount;
            if (Health <= 0 ) {
                Die();
            }
        }

        void Die() {
            if (OnDie != null) {
                OnDie.Invoke();
            }
        }
    }
}
