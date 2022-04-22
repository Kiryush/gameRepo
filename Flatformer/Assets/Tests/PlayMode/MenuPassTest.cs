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
            var gameObject = new GameObject(); //Создаем объект на тестовой сцене
            var lvlManager = gameObject.AddComponent<LevelManager>(); //Добавляем к нему компонент для переключения сцен и проводим ссылку на него
            lvlManager.StartGame(); //Стартуем сцену с игровым уровнем
            yield return new WaitForSeconds(1.0f); //Ждем пару секунд
            Assert.AreEqual(GameSceneIndex, SceneManager.GetActiveScene().buildIndex); //Проверяем, совпадает ли текущая сцена с указанным игровым уровнем
        }
        
        [UnityTest]
        public IEnumerator SpawnPlayerAndFreeFall()
        {
            var player = new GameObject(); //Создаем объект для игрока на тестовой сцене
            
            var playerCollider = player.AddComponent<BoxCollider>(); //Добавляем к нему коллайдер для проверки на пересечение с другим объектом
            var playerPrefs = player.AddComponent<Player>(); //Добавляем к объекту компонент игрока для обработки сближения с другим объектом
            var controller = player.AddComponent<CharacterController>(); //Контроллер для перемещения игрока с учетом гравитации
            
            playerPrefs.GroundDistance = 0.2f; //Расстояние до земли для учета приземления игрока
            playerPrefs.Controller = controller; //Добавляем контроллер игроку
            playerCollider.isTrigger = false; //Включаем триггер, чтобы проверять столкновение с объектом
            playerCollider.size = new Vector3(3, 3, 3); //Определяем размер коллайдера
            player.transform.position = new Vector3(0, 3, 0); //Определяем позицию игрока
            playerPrefs.Step = new GameObject().GetComponent<Transform>(); //Создаем пустой объект для отслеживания поверхности
            playerPrefs.Step.position = new Vector3(0, 1f, 0); //Определяем позицию этого объекта
            
            yield return new WaitForSeconds(5.0f); //Ждем несколько секунд для проверки на столкновение с объектом
            Assert.AreEqual(false, playerPrefs.IsGrounded); //Результат должен быть отрицательным
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
