using UnityEngine;
using UnityEngine.Splines;
using Utilities;

namespace DragonWorm {
    public class EnemyBuilder {
        GameObject enemyPrefab;
        SplineContainer spline;
        GameObject weaponPrefab;

        float speed;

        public EnemyBuilder SetBasePrefab(GameObject prefab) {
            enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpline(SplineContainer spline) {
            this.spline = spline;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject weaponPrefab) {
            this.weaponPrefab = weaponPrefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed) {
            this.speed = speed;
            return this;
        }

        public GameObject Build() {
            GameObject instance = GameObject.Instantiate(enemyPrefab);

            SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();
            splineAnimate.Container = spline;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.NegativeZAxis;
            splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeXAxis;
            splineAnimate.MaxSpeed = speed;

            instance.transform.position = spline.EvaluatePosition(0);

            splineAnimate.Play();

            return instance;
        }
    }
}
