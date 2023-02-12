using UnityEngine;

[RequireComponent(typeof(MonsterFinder))]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected GameObject m_projectilePrefab;
    [SerializeField] protected Transform m_shootPoint;
    [SerializeField] protected float m_shootInterval;
    [SerializeField] protected float m_range;
    [SerializeField] private MonsterFinder m_monsterFinder;

    protected IProjectileFactory m_projectileFactory;
    protected GameObject m_currentEnemy;
    protected Vector3 m_enemyVelocity = Vector3.zero;
    protected float m_lastShootTime;

    public GameObject ProjectilePrefab => m_projectilePrefab;

    public void Construct(IProjectileFactory projectileFactory) =>
        m_projectileFactory = projectileFactory;

    private void Awake() => 
        m_monsterFinder.SetRange(m_range);
    
    protected bool CanShoot()
    {
        if (!m_projectilePrefab || !m_shootPoint) 
            return false;
        
        if (!m_currentEnemy || !m_currentEnemy.activeInHierarchy)
        {
            m_currentEnemy = m_monsterFinder.GetMonster;
            if (!m_currentEnemy) return false;
            
            if(m_enemyVelocity == Vector3.zero)
                m_enemyVelocity = m_currentEnemy.GetComponent<MonsterMover>().GetVelocity;
        }

        if (IsEnemyOutOfRange())
        {
            m_currentEnemy = null;
            return false;
        }

        return true;
    }

    private bool IsEnemyOutOfRange()
    {
        return (transform.position-m_currentEnemy.transform.position).sqrMagnitude > m_range * m_range;
    }
}