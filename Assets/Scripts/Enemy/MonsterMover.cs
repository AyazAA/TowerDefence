using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterMover : MonoBehaviour
{
	[SerializeField] private Rigidbody m_rigidbody;
	[SerializeField] private float m_speed = 5f;
	private GameObject m_moveTarget;
	
	public Vector3 GetVelocity => m_rigidbody.velocity;
	
	public void SetTarget(GameObject moveTarget) => 
		m_moveTarget = moveTarget;

	private void Start() => 
		StartMoving();

	private void StartMoving()
	{
		if (m_moveTarget == null)
			Debug.Log("Set target for moving");

		Vector3 direction = m_moveTarget.transform.position - gameObject.transform.position;
		m_rigidbody.velocity = direction.normalized * m_speed;
	}
}