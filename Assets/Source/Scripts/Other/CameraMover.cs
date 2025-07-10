using UnityEngine;

namespace Source.Scripts.Other
{
    public class CameraMover : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        [SerializeField] private float _speed;

        private void Update()
        {
            float horizontal = Input.GetAxis(HorizontalAxis);
            float vertical = Input.GetAxis(VerticalAxis);
        
            transform.Translate(horizontal * _speed * Time.deltaTime, 0, vertical * _speed * Time.deltaTime);
        }
    }
}
