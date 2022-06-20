using Joystick;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class MovementController : ITickable
    {
        private readonly JoystickController _joystickController;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        
        public MovementController(JoystickController joystickController, Rigidbody rigidbody, float speed)
        {
            _joystickController = joystickController;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void Tick()
        {
            _rigidbody.velocity = _joystickController.InputDirection * _speed;

            var direction = _rigidbody.velocity;
            if (!direction.Equals(Vector3.zero))
            {
                _rigidbody.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}