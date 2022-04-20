using UnityEngine;

namespace SceneManagment
{
    public class SpinAround : MonoBehaviour
    {
        public Vector3 _desiredRotation;
        private Vector3 _currentRotation;
        private void Update()
        {
            _currentRotation += _desiredRotation;
            transform.rotation = Quaternion.Euler(_currentRotation);
        }
    }
}
