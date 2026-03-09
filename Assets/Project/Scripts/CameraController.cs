using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonWorm {
    public class CameraController : MonoBehaviour {
        [SerializeField] Transform player;
        [SerializeField] float speed = 2f;

        private void Start() {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }

        private void LateUpdate() {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
