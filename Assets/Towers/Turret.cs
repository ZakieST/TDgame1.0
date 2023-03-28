using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    public GameObject bulletPrefab;
    public Transform attackPoint;
    public float range = 15f;
    public float shootingTimer = 0.5f;

    public string enemyTag = "Enemy";

    private bool _facingRight = false;
    private float shootingTimerCount = 0f;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    void Update()
    {
        float enemyX = 0f;
        if (target == null)
        {
            
        }
        else
        {
            enemyX = target.transform.position.x - transform.position.x;
            Vector3 rotation = target.position - attackPoint.position;
            attackPoint.rotation = attackPoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
            if (shootingTimerCount <= 0)
            {
                Shoot();
                shootingTimerCount = shootingTimer;
            }
        }
        
        if(enemyX > 0 && !_facingRight)
        {
            Flip();
        }
        if (enemyX < 0 && _facingRight)
        {
            Flip();
        }

        shootingTimerCount -= Time.deltaTime;
    }
    
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        _facingRight = !_facingRight;
    }

    private void Shoot()
    {
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        TurretBulletScript bullet = bulletObj.GetComponent<TurretBulletScript>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
