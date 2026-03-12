using UnityEngine;
using UnityEngine.UIElements;

namespace DragonWorm {
    public class GameUIController : MonoBehaviour {
        VisualElement root;
        VisualElement healthFill;

        Player player;
        Life life;

        void Start() {
            var uiDocument = GetComponent<UIDocument>();
            root = uiDocument.rootVisualElement;

            healthFill = root.Q<VisualElement>("HealthFill");

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            life = player.Life;

            UpdateHealth();
        }

        void Update() {
            UpdateHealth();
        }

        void UpdateHealth() {
            float percent = (float)life.Health / life.MaxHealth;
            healthFill.style.width = Length.Percent(percent * 100f);
        }
    }
}
