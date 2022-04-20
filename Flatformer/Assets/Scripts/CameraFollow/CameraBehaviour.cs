using UnityEngine;

namespace CameraFollow
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        public void Update()
        {
            transform.position = target.position - offset;
        }
    }
}
