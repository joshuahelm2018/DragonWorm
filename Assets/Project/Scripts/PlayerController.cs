using UnityEngine;

namespace DragonWorm {
    public class PlayerController : MonoBehaviour {
        [SerializeField] float speed = 5f;
        [SerializeField] float smoothness = 0.1f;

        [SerializeField] GameObject model;

        [Header("Camera Bounds")]
        [SerializeField] Transform cameraFollow;
        [SerializeField] float minX = -4f;
        [SerializeField] float maxX =  4f;
        [SerializeField] float minY = -8f;
        [SerializeField] float maxY = 8f;

        InputReader input;

        Vector3 currentVelocity;
        Vector3 targetPosition;

        private void Start() {
            input = GetComponent<InputReader>();

            if (cameraFollow == null) {
                cameraFollow = Camera.main.transform;
            }
        }

        private void Update() {
            targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);

            float minPlayerX = cameraFollow.position.x + minX;
            float maxPlayerX = cameraFollow.position.x + maxX;
            float minPlayerY = cameraFollow.position.y + minY;
            float maxPlayerY = cameraFollow.position.y + maxY;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
        }
    }
}
