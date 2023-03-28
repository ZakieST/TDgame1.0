using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletScript : MonoBehaviour
{
    public Vector3 mousePos;
    private Rigidbody2D _rb;

    private float _lifeTime = 1f;
    public float force;
    [SerializeField]
    private int _damage = 15;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        FireBullet();
    }

    private void Update()
    {
        if (_lifeTime <= 0)
            Destroy(gameObject);
        _lifeTime -= Time.deltaTime;
    }

    private void FireBullet()
    {
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name != "Player")
        {
            if(collider.gameObject.TryGetComponent<HealthScript>(out var health))
            {
                health.Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
