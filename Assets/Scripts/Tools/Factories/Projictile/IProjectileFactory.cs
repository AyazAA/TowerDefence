using UnityEngine;

public interface IProjectileFactory
{
    public GameObject CreateProjectile(Transform shootPoint);
}