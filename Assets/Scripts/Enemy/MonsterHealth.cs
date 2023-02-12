using System;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public event Action<GameObject> MonsterDied;
    [SerializeField] private int m_maxHP = 30;
    private int m_hp;

    private void OnEnable() => 
        m_hp = m_maxHP;

    public void TakeDamage(int damage)
    {
        m_hp -= damage;
        if (m_hp <= 0)
        {
            MonsterDied?.Invoke(gameObject);
            gameObject.SetActive(false);
        } 
    }
}