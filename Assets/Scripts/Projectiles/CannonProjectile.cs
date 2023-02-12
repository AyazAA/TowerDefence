using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonProjectile : MonoBehaviour 
{
	[SerializeField] private float m_speed = 10f;
	[SerializeField] private Rigidbody m_rigidbody;

	public float Speed => m_speed;

	private void OnEnable() => 
		SetMovingDirection();

	private void SetMovingDirection() => 
		m_rigidbody.velocity = transform.forward * m_speed;
}