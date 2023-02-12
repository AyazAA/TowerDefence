using System;
using UnityEngine;

public class MonsterReachTargetDetector : MonoBehaviour
{
    public event Action<GameObject> MonsterReachedTarget;
    private GameObject m_moveTarget;
    private const float m_reachDistance = 1f;
	
    private void Update () => 
        CheckingTargetReach();
	
    public void SetTarget(GameObject moveTarget) => 
        m_moveTarget = moveTarget;
	
    private void CheckingTargetReach()
    {
        if (IsTargetReached())
        {
            MonsterReachedTarget?.Invoke(gameObject);
            gameObject.SetActive(false);
        }
    }

    private bool IsTargetReached() => 
        (transform.position-m_moveTarget.transform.position).sqrMagnitude <= m_reachDistance * m_reachDistance;
}