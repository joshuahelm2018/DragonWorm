using UnityEngine;

namespace DragonWorm {
    public class Edible : MonoBehaviour {
        Actor actor;
        private void Start() {
            actor = GetComponent<Actor>();
        }

        public void EatenBy(Actor predator) {
            if (actor) {
                actor.TakeMaxDamage();
            }
        }
    }
}