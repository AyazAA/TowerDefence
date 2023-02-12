using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private Spawner m_spawner;
    [SerializeField] private CannonTower m_cannonTower;
    [SerializeField] private SimpleTower m_simpleTower;
    
    private void Awake()
    {
        IEnemyFactory enemyFactory = new EnemyFactory(m_spawner.MonsterPrefab, m_spawner.MoveTarget, m_spawner.transform.position);
        m_spawner.Construct(enemyFactory);

        IProjectileFactory cannonProjectileFactory = new CannonProjectileFactory(m_cannonTower.ProjectilePrefab);
        m_cannonTower.Construct(cannonProjectileFactory);
        
        IProjectileFactory guidedProjectileFactory = new GuidedProjectileFactory(m_simpleTower.ProjectilePrefab);
        m_simpleTower.Construct(guidedProjectileFactory);
    }
}