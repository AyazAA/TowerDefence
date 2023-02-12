using UnityEngine;

public class ProjectileCollisionDetector : MonoBehaviour
{
    [SerializeField] private int m_damage = 10;
	
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MonsterHealth>(out var monster))
        {
            monster.TakeDamage(m_damage);
            gameObject.SetActive(false);
        }
		
        if(other.gameObject.TryGetComponent<Ground>(out var ground))
            gameObject.SetActive(false);
    }
}