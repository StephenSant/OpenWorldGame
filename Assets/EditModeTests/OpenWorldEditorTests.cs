using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class OpenWorldEditorTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void OpenWorldEditorTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [Test]
        public void Player_Exist_By_Type()
        {
            // Test to see if there is a gameobject with the Player Component.
            Player player = GameObject.FindObjectOfType<Player>();
            Assert.IsTrue(player != null);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Player_Got_Hurt()
        {
            var player = Object.FindObjectOfType<Player>();
            var oldHealth = player.health;
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
            Assert.IsTrue(oldHealth > player.health);
        }
    }
}
