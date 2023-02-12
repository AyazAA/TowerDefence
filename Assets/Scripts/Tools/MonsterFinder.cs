using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class MonsterFinder : MonoBehaviour
{
    [SerializeField] private SphereCollider m_zoneCollider;
    private List<GameObject> m_monsters = new List<GameObject>();
    
    public GameObject GetMonster => m_monsters.FirstOrDefault();

    public void SetRange(float range) => 
        m_zoneCollider.radius = range;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MonsterHealth>(out var monster))
        {
            m_monsters.Add(monster.gameObject);    
            monster.MonsterDied += OnMonsterOut;
            monster.GetComponent<MonsterReachTargetDetector>().MonsterReachedTarget += OnMonsterOut;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MonsterHealth>(out var monster)) 
            OnMonsterOut(monster.gameObject);
    }

    private void OnMonsterOut(GameObject monster)
    {
        monster.GetComponent<MonsterHealth>().MonsterDied -= OnMonsterOut;
        monster.GetComponent<MonsterReachTargetDetector>().MonsterReachedTarget -= OnMonsterOut;
        m_monsters.Remove(monster.gameObject);
    }
}