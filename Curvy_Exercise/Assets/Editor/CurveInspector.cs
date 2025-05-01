using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Curve))]
public class CurveInspector : Editor
{
	private Curve m_Curve;
	private Transform m_tHandleTransform;
	private Quaternion m_qHandleRotation;

	[SerializeField] private const int m_iDebugDrawLineSteps = 15;

	private void OnSceneGUI()
	{
		m_Curve = target as Curve;
		m_tHandleTransform = m_Curve.transform;
		m_qHandleRotation = Tools.pivotRotation == PivotRotation.Local ? m_tHandleTransform.rotation : Quaternion.identity;

		Handles.color = Color.yellow;
		for (int i = 0; i < m_Curve.points.Length; i++)
		{
			if (i > 0)
			{
				ShowPoint(i);
				Handles.DrawLine(m_Curve.points[i - 1], m_Curve.points[i]);
			}
		}

		Vector3 lineStart = m_Curve.GetPointFromArray(0f);

		Handles.color = Color.white;
		for (int i = 1; i <= m_iDebugDrawLineSteps; i++)
		{
			Vector3 lineEnd = m_Curve.GetPointFromArray(i / (float)m_iDebugDrawLineSteps);
			Handles.DrawLine(lineStart, lineEnd);
			lineStart = lineEnd;
		}
	}


	Vector3 ShowPoint(int index)
	{
		Vector3 point = m_tHandleTransform.TransformPoint(m_Curve.points[index]);


		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, m_qHandleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(m_Curve, "Move Point");
			EditorUtility.SetDirty(m_Curve);
			m_Curve.points[index] = m_tHandleTransform.InverseTransformPoint(point);
		}


		return point;
	}
}
