using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace DragonWorm {
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour {
        PlayerInput playerInput;
        InputAction moveAction;
        InputAction fireAction;

        public Vector2 Move => moveAction.ReadValue<Vector2>();
        public bool Attack => fireAction.ReadValue<float>() > 0f;

        private void Awake() {
            if (TryGetComponent(out playerInput)) {
                moveAction = playerInput.actions["Move"];
                fireAction = playerInput.actions["Attack"];
            }

            Debug.Log(playerInput.currentActionMap);
        }

        private void Start() {
            StartCoroutine(StartInput());
        }

        IEnumerator StartInput() {
            yield return null;

            playerInput.DeactivateInput();
            playerInput.ActivateInput();
        }
    }
}
