using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        Debug.Log("Good");
        SceneManager.LoadScene(1);
    }
}
