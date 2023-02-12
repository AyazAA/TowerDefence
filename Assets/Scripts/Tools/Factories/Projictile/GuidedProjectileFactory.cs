using UnityEngine;

public class GuidedProjectileFactory : IProjectileFactory
{
    private readonly ObjectPool<GuidedProjectile> m_projectilePool;
    
    public GuidedProjectileFactory(GameObject projectilePrefab)
    {
        GuidedProjectile guidedProjectile = projectilePrefab.GetComponent<GuidedProjectile>();
        m_projectilePool = new ObjectPool<GuidedProjectile>(guidedProjectile);
    }
    
    public GameObject CreateProjectile(Transform shootPoint)
    {
        GuidedProjectile guidedProjectile = m_projectilePool.GetPooledObject();
        guidedProjectile.transform.position = shootPoint.position;
        guidedProjectile.transform.rotation = Quaternion.identity;
        guidedProjectile.gameObject.SetActive(true);
        return guidedProjectile.gameObject;
    }
}