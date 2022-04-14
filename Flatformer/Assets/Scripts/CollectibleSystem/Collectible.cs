using UnityEngine;

namespace CollectibleSystem
{
    public class Collectible : MonoBehaviour
    {
        public Vector3 desiredRotation;
        private Vector3 currentRotation;
        private void Update()
        {
            currentRotation += desiredRotation;
            transform.rotation = Quaternion.Euler(currentRotation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;
            other.transform.GetComponent<Player>().CollectSpaceShard();
            Destroy(gameObject);
        }
    }
}
