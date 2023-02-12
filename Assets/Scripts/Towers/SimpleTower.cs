using UnityEngine;

public class SimpleTower : Tower 
{
	private void Update () 
	{
		if (CanShoot()) 
			Shoot();
	}

	private void Shoot()
	{
		if (m_lastShootTime + m_shootInterval > Time.time)
			return;

		GuidedProjectile projectile = m_projectileFactory.CreateProjectile(m_shootPoint).GetComponent<GuidedProjectile>();
		projectile.SetTarget(m_currentEnemy.gameObject);

		m_lastShootTime = Time.time;
	}
}
