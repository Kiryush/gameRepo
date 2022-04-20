using System.Collections;
using CollectibleSystem;
using NUnit.Framework;
using SceneManagment;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class MenuPassTest
    {
        private const int GameSceneIndex = 1;
        
        [UnityTest]
        public IEnumerator EnterGame()
        {
            var gameObject = new GameObject();
            var lvlManager = gameObject.AddComponent<LevelManager>();
            lvlManager.StartGame();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(GameSceneIndex, SceneManager.GetActiveScene().buildIndex);
        }
        
        [UnityTest]
        public IEnumerator SpawnPlayerAndFreeFall()
        {
            var player = new GameObject();
            
            var playerCollider = player.AddComponent<BoxCollider>();
            var playerPrefs = player.AddComponent<Player>();
            var controller = player.AddComponent<CharacterController>();
            
            player.transform.tag = "Player";
            playerPrefs.GroundDistance = 0.2f;
            playerPrefs.Controller = controller;
            playerCollider.isTrigger = true;
            playerCollider.size = new Vector3(3, 3, 3);
            player.transform.position = new Vector3(0, 3, 0);
            playerPrefs.Step = new GameObject().GetComponent<Transform>();
            playerPrefs.Step.position = new Vector3(0, 1f, 0);
            
            yield return new WaitForSeconds(5.0f);
            Assert.AreEqual(false, playerPrefs.IsGrounded);
        }
        
        [UnityTest]
        public IEnumerator CheckCollectibles()
        {
            var player = new GameObject();
            
            var playerCollider = player.AddComponent<BoxCollider>();
            var playerPrefs = player.AddComponent<Player>();
            var controller = player.AddComponent<CharacterController>();
            
            player.transform.tag = "Player";
            playerPrefs.GroundDistance = 0.2f;
            playerPrefs.Controller = controller;
            playerCollider.isTrigger = true;
            playerCollider.size = new Vector3(3, 3, 3);
            player.transform.position = new Vector3(0, 3, 0);
            playerPrefs.Step = new GameObject().GetComponent<Transform>();
            playerPrefs.Step.position = new Vector3(0, 1.5f, 0);
            
            var rb = player.AddComponent<Rigidbody>();
            rb.freezeRotation = true;

            var collectible = new GameObject();
            collectible.AddComponent<Collectible>();
            var collectibleCollider = collectible.AddComponent<BoxCollider>();
            collectibleCollider.isTrigger = true;
            collectibleCollider.size = new Vector3(3, 3, 3);
            
            yield return new WaitForSeconds(3.0f);
            Assert.AreEqual(1, playerPrefs.SpaceShards);
        }
    }
}
