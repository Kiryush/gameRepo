using UnityEngine;

namespace CollectibleSystem
{
    public class Collectible : MonoBehaviour
    {
        public Vector3 _desiredRotation;
        private Vector3 _currentRotation;
        private void Update()
        {
            _currentRotation += _desiredRotation;
            transform.rotation = Quaternion.Euler(_currentRotation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;
            other.transform.GetComponent<Player>().CollectSpaceShard();
            Destroy(gameObject);
        }
    }
}
