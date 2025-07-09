using UnityEngine;

namespace Source.Scripts
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        
            transform.Translate(horizontal * _speed * Time.deltaTime, 0, vertical * _speed * Time.deltaTime);
        }
    }
}
