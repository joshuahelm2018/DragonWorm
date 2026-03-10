using UnityEngine;

namespace DragonWorm {
    [CreateAssetMenu(fileName = "EnemyType", menuName = "DragonWorm/EnemyyType", order = 0)]
    public class EnemyType : ScriptableObject {
        public GameObject enemyPrefab;
        public GameObject weaponPrefab;
        public float speed;
    }
}
