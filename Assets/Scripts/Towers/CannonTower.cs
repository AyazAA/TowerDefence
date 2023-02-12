using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private float m_rotationSpeed = 30f;
    private bool m_towerReady = false;
    private float m_projectileSpeed;


    private void Start()
    {
        CannonProjectile cannonProjectile = m_projectilePrefab.GetComponent<CannonProjectile>();
        m_projectileSpeed = cannonProjectile.Speed;
    }

    private void Update()
    {
        if (CanShoot())
        {
            RotateCanon();
            Shoot();
        }
        else
            m_towerReady = false;
    }

    private void RotateCanon()
    {
        Quaternion rotation = transform.rotation;

        Vector3 shootDirection = ShootDirectionCalculator.Calculate(m_currentEnemy.transform.position, m_shootPoint.position, m_enemyVelocity,
            m_projectileSpeed, m_range);

        if (shootDirection != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(shootDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * m_rotationSpeed);
            m_towerReady = (transform.rotation == rotation);
        }
    }

    private void Shoot()
    {
        if (m_lastShootTime + m_shootInterval > Time.time || !m_towerReady)
            return;

        m_projectileFactory.CreateProjectile(m_shootPoint);
        
        m_lastShootTime = Time.time;
        m_towerReady = false;
    }
}