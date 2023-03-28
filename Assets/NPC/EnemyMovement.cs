using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float vision = 1.25f;
    public float attackTimer = 1f;
    public int damage = 10;

    private Transform _target;
    private int _wavepointIndex = 0;
    private GameObject _player;
    private string _playerTag = "Player";
    private float attackTimerCounter = 0f;

    void Start()
    {
        _target = Waypoints.points[0];
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    void FixedUpdate()
    {
        Vector3 dirToPlayer = _player.transform.position - transform.position;
        //float distanceThisFrame = speed * Time.deltaTime;

        if (dirToPlayer.magnitude <= vision)
        {
            if ((dirToPlayer.magnitude <= 0.75f || Mathf.Abs(dirToPlayer.y) <= 0.9f) && attackTimerCounter <= 0)
            {
                Hit();
                attackTimerCounter = attackTimer;
            }
            else
            {
                transform.Translate(dirToPlayer.normalized * speed * (dirToPlayer.magnitude - 0.75f) * Time.deltaTime, Space.World);
            }
        }
        else
        {
            Vector3 dir = _target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
        }

        attackTimerCounter -= Time.deltaTime;
        if(gameObject.TryGetComponent<HealthScript>(out var health))
            if (health.Hp <= 0)
                Destroy(gameObject);
    }

    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        
        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];   

    }

    private void Hit()
    {
        if(_player.TryGetComponent<HealthScript>(out var health))
        {
            health.Damage(damage);
        }
    }
}
