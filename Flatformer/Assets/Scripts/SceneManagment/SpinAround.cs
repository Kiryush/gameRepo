using UnityEngine;

namespace SceneManagment
{
    public class SpinAround : MonoBehaviour
    {
        public Vector3 desiredRotation;
        private Vector3 currentRotation;
        private void Update()
        {
            currentRotation += desiredRotation;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
