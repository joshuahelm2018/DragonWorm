using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DragonWorm.Tests {

    public class EditorTests {
        [Test]
        public void LifeIsDeadWhenReachesZero() {
            Life life = new Life(1);

            life.TakeDamage(1);

            Assert.IsFalse(life.IsAlive);
        }

        [Test]
        public void PlayerIsDeadWhenHealthReachesZero() {
            GameObject playerGO = new GameObject();
            Life life = playerGO.AddComponent<Life>();
            Player player = playerGO.AddComponent<Player>();

            player.Initialize();

            player.Life.SetHealth(1);
            player.Life.TakeDamage(1);
            
            Assert.IsFalse(player.Life.IsAlive);
        }
    }
}
