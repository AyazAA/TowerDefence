using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private MonsterMover m_monsterPrefab;
	[SerializeField] private GameObject m_moveTarget;
	[SerializeField] private float m_interval = 3;
	private IEnemyFactory m_factory;
	private float m_lastSpawn = 0;

	public MonsterMover MonsterPrefab => m_monsterPrefab;
	public GameObject MoveTarget => m_moveTarget;
	
	public void Construct(IEnemyFactory factory) => 
		m_factory = factory;

	private void Update () 
	{
		if (CanSpawn())
		{
			m_factory.CreateEnemy();
			m_lastSpawn = Time.time;
		}
	}

	private bool CanSpawn() => 
		Time.time > m_lastSpawn + m_interval;
}
