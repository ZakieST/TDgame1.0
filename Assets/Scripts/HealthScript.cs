using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int _maxHp = 100;
    [SerializeField]
    private int _hp;
    private HealthBar _healthBar;

    public int Hp
    {
        get => _hp;
        private set
        {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, 0, _maxHp);
            if (isDamage)
            {
                Damaged?.Invoke(_hp);
            }
            else
            {
                Healed?.Invoke(_hp);
            }

            _healthBar.UpdateHealthBar(_maxHp, _hp);

            if (_hp <= 0)
            {
                Died?.Invoke(_hp);
            }
        }
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Died;

    private void Awake()
    {
        _hp = _maxHp;
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void Damage(int amount) 
        => Hp -= amount;

    public void Heal(int amount) 
        => Hp += amount;

    public void HealFull() 
        => Hp = _maxHp;

    public void Kill() 
        => Hp = 0;

    public void Adjust(int value) 
        => Hp = value;
    
    public void Lethal() 
        => Destroy(gameObject);
}
