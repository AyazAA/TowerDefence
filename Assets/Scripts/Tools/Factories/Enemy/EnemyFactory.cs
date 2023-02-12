using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private readonly ObjectPool<MonsterMover> m_monsterPool;
    private readonly GameObject m_moveTarget;
    private readonly Vector3 m_spawnPosition;
    
    public EnemyFactory(MonsterMover monsterPrefab, GameObject moveTarget, Vector3 spawnPosition)
    {
        m_monsterPool = new ObjectPool<MonsterMover>(monsterPrefab);
        m_moveTarget = moveTarget;
        m_spawnPosition = spawnPosition;
    }
    
    public GameObject CreateEnemy()
    {
        MonsterMover monsterMover = m_monsterPool.GetPooledObject();
        monsterMover.transform.position = m_spawnPosition;
        monsterMover.transform.rotation = Quaternion.identity;
        monsterMover.SetTarget(m_moveTarget);
        MonsterReachTargetDetector monsterReachTargetDetector = monsterMover.GetComponent<MonsterReachTargetDetector>();
        monsterReachTargetDetector.SetTarget(m_moveTarget);
        monsterMover.gameObject.SetActive(true);
        return monsterMover.gameObject;
    }
}