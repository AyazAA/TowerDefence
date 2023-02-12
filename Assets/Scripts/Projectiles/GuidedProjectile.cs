using UnityEngine;

public class GuidedProjectile : MonoBehaviour {
	[SerializeField] private float m_speed = 10f;
	private GameObject m_target;

	private void Update ()
	{
		if (!IsTargetAvailable())
			return;

		Move();
	}

	public void SetTarget(GameObject target) => 
		m_target = target;

	private void Move()
	{
		var translation = m_target.transform.position - transform.position;
		translation = translation.normalized * m_speed;
		transform.Translate(translation * Time.deltaTime);
	}

	private bool IsTargetAvailable()
	{
		if (m_target == null) {
			gameObject.SetActive(false);
			return false;
		}

		return true;
	}
}
