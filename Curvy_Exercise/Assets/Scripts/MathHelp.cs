using Vector3 = UnityEngine.Vector3;
using UnityEngine;

public static class MathHelp
{
	public static Vector3 GetCurvePoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
	}

	public static Vector3 GetCurvePointFromArray(Vector3[] points, float t)
	{
		if (points.Length == 1)
		{
			return points[0];
		}
		if (points.Length == 2)
		{
			return Vector3.Lerp(points[0], points[1], t);
		}
		if (points.Length == 3)
		{
			return GetCurvePoint(points[0], points[1], points[2], t);
		}

		Vector3[] newPoints = new Vector3[points.Length - 1];
		for (int i = 0; i < points.Length - 1; i++)
		{
			newPoints[i] = Vector3.Lerp(points[i], points[i + 1], t);
		}
		return GetCurvePointFromArray(newPoints, t);
	}
}
