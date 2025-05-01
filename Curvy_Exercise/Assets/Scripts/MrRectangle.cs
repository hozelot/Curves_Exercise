using UnityEngine;

public class MrRectangle : MonoBehaviour
{
	[SerializeField] private Curve _curve;
	[SerializeField] private float _speed = 0.5f;
	private float t = 0f;

	private void Update()
	{
		if (_curve.points.Length >= 3)
		{
			t += Time.deltaTime * _speed;
			// Start moving backwards when Mr Rectangle reaches the endpoint
			if (t > 1f)
			{
				_speed = -_speed;
			}
			// Start moving forward again when Mr Rectangle reaches the start point
			else if (t < 0f)
			{
				_speed = Mathf.Abs(_speed);
			}

			Vector3 point = _curve.GetPointFromArray(t);
			transform.position = point;
		}
	}
}
