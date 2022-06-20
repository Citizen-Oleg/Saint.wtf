using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Joystick
{
    public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Vector3 InputDirection { get; private set; }
    
        [SerializeField]
        private Image _joystickBorder;
        [SerializeField]
        private Image _joystickCircle;
        [SerializeField]
        private float _offset;
    
        private Vector2 _startPosition;
    
        private void Start()
        {
            _startPosition = _joystickBorder.transform.position;
        }
    
        public void OnPointerDown(PointerEventData eventData)
        {
            MoveJoystickToTapPosition();
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBorder.rectTransform,
                eventData.position, eventData.pressEventCamera, out var position);
    
            var size = _joystickBorder.rectTransform.rect.size;
            var backgroundImageSizeX = size.x;
            var backgroundImageSizeY = size.y;
    
            position.x /= backgroundImageSizeX;
            position.y /= backgroundImageSizeY;
    
            InputDirection = new Vector3(position.x, 0, position.y).normalized;
    
            _joystickCircle.rectTransform.anchoredPosition = new
                Vector2(position.x * (backgroundImageSizeX / _offset),
                    position.y * (backgroundImageSizeY / _offset));
        }
    
        public void OnPointerUp(PointerEventData eventData)
        {
            ReturnJoystickToStartPosition();
            InputDirection = Vector2.zero;
        }
            
        private void MoveJoystickToTapPosition()
        {
            var position = Input.mousePosition;
            _joystickBorder.transform.position = position;
        }
    
        private void ReturnJoystickToStartPosition()
        {
            _joystickBorder.transform.position = _startPosition;
            _joystickCircle.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
