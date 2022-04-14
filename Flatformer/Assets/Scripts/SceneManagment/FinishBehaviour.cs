using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public class FinishBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;
            Debug.Log("Good");
            SceneManager.LoadScene(2);
        }
    }
}