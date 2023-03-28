using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public enum MouseButton
    {
        Left, Right
    }
    
    public class MouseInput : MonoBehaviour
    {
        private Controls _inputAction;
        
        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseInWorldPosition => Camera.main.ScreenToWorldPoint(MousePosition);

        private bool _isLeftButtonPressed;
        private bool _isRightButtonPressed;
        
        private void OnEnable()
        {
            _inputAction = Controls.Instance;
            _inputAction.Gameplay.MousePos.performed += OnMousePosPerformed;
            _inputAction.Gameplay.Shoot.performed += OnShootPerformed;
            _inputAction.Gameplay.Shoot.canceled += OnShootCanceled;
            _inputAction.Gameplay.Destroy.performed += OnDestroyPerformed;
            _inputAction.Gameplay.Destroy.canceled += OnDestroyCanceled;
        }

        private void OnDisable()
        {
            _inputAction.Gameplay.MousePos.performed -= OnMousePosPerformed;
            _inputAction.Gameplay.Shoot.performed -= OnShootPerformed;
            _inputAction.Gameplay.Shoot.canceled -= OnShootCanceled;
            _inputAction.Gameplay.Destroy.performed -= OnDestroyPerformed;
            _inputAction.Gameplay.Destroy.canceled -= OnDestroyCanceled;
        }

        private void OnMousePosPerformed(InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }

        private void OnShootPerformed(InputAction.CallbackContext ctx)
        {
            _isLeftButtonPressed = true;
        }
        
        private void OnShootCanceled(InputAction.CallbackContext ctx)
        {
            _isLeftButtonPressed = false;
        }
        
        private void OnDestroyPerformed(InputAction.CallbackContext ctx)
        {
            _isRightButtonPressed = true;
        }
        
        private void OnDestroyCanceled(InputAction.CallbackContext ctx)
        {
            _isRightButtonPressed = false;
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            return button == MouseButton.Left ? _isLeftButtonPressed : _isRightButtonPressed;
        }
    }
}

