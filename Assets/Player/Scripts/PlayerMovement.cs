using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    private bool _facingRight = true;
    
    public Controls _controls;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Camera _cam;
    
    private Vector2 _movement;
    private Vector2 _mousePos;

    private void Awake()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rb = GetComponent<Rigidbody2D>();
        _controls = new Controls();
        _animator = GetComponent<Animator>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
    
    public void OnMousePos(InputAction.CallbackContext context)
    {
        _mousePos = _cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    void FixedUpdate() 
    {
        _rb.velocity = _movement * _moveSpeed * Time.deltaTime;
        
        Flipping();
        WalkingAnimationSet();
        
    }

    private void WalkingAnimationSet()
    {
        if (_movement.x != 0 || _movement.y != 0) 
        {
            _animator.SetFloat("Speed", 1);
        } else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
    
    
    private void Flipping()
    {
        Vector2 lookDir = _mousePos - _rb.position;
        if(lookDir.x > 0 && !_facingRight)
        {
            Flip();
        }
        if (lookDir.x < 0 && _facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        _facingRight = !_facingRight;
    }
}
