using BuildingSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using GameInput;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private BuildingPlacer _buildingPlacer;
    
    private Animator _animator;
    private Camera _cam;
    private Controls _controls;

    public Transform attackPoint;
    public GameObject bulletPrefab;

    private Vector3 _mousePos;

    public float secondsBetweenFiring = 0.75f;
    private float _timeCounter = 0f;

    void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _controls = new Controls();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    { 
        if (_timeCounter > -10)
            _timeCounter -= Time.fixedDeltaTime;
    }

    public void OnShoot()
    {
        if (_timeCounter > 0 || _buildingPlacer.ActiveBuildableTower != null)
            return;
        Vector3 rotation = _mousePos - attackPoint.position;
        float rotZ = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(0, 0, rotZ);

       _animator.SetTrigger("Attack");
       var bullet = (GameObject)Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
       bullet.GetComponent<BulletScript>().mousePos = _mousePos;
       _timeCounter = secondsBetweenFiring;
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        _mousePos = _cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
