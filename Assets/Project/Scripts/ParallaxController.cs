using UnityEngine;

namespace DragonWorm {
    public class ParallaxController : MonoBehaviour {
        [SerializeField] Transform[] backgrounds;
        [SerializeField] float smoothing = 10f;
        [SerializeField] float multiplier = 1f;

        Transform cam;
        Vector3 previousCamPos;

        private void Awake() {
            cam = Camera.main.transform;
        }

        private void Start() {
            previousCamPos = cam.position;
        }

        private void Update() {
            for (int i= 0; i<backgrounds.Length; i++) {
                Transform background = backgrounds[i];
                float parallax = (previousCamPos.x - cam.position.x) * (i * multiplier);
                float targetX = background.position.x + parallax;

                var targetPos = new Vector3(targetX, background.position.y, background.position.z);

                background.position = Vector3.Lerp(background.position, targetPos, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }
    }
}
