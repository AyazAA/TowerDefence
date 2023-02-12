using UnityEngine;

public class CannonProjectileFactory : IProjectileFactory
{
    private readonly ObjectPool<CannonProjectile> m_projectilePool;
    
    public CannonProjectileFactory(GameObject projectilePrefab)
    {
        CannonProjectile cannonProjectile = projectilePrefab.GetComponent<CannonProjectile>();
        m_projectilePool = new ObjectPool<CannonProjectile>(cannonProjectile);
    }
    
    public GameObject CreateProjectile(Transform shootPoint)
    {
        CannonProjectile cannonProjectile = m_projectilePool.GetPooledObject();
        cannonProjectile.transform.position = shootPoint.position;
        cannonProjectile.transform.rotation = shootPoint.rotation;
        cannonProjectile.gameObject.SetActive(true);
        return cannonProjectile.gameObject;
    }
}