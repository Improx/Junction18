using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float Angle = 75;
	public float MaxDistance = 5;
	public float Resolution = 100;
	public MeshFilter ViewMeshFilter;
	public LayerMask RayCastMask;
	public float MaskCutawayDistance = 0.18f;
	[HideInInspector] public List<DummyRobber> VisibleRobbers = new List<DummyRobber>();

	private Mesh _viewMesh;

	// Use this for initialization
	void Start () {
		_viewMesh = new Mesh();
		_viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = _viewMesh;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		DrawFieldOfView();
		DetectRobbers();
	}

	private void DrawFieldOfView()
	{
		float viewDirectionAngle = GetViewDirectionAngle();
		float lowAngle = viewDirectionAngle - Angle / 2;
		float highAngle = viewDirectionAngle + Angle / 2;
		int numRays = Mathf.RoundToInt(Angle * Resolution);
		float angleIncrement = Angle / numRays;

		//Shoot rays and save hit points:
		List<Vector3> hitPoints = new List<Vector3>();
		for (int i = 0; i < numRays; i++)
		{
			float rayAngle = lowAngle + angleIncrement * i;
			Vector3 rayDir = AngleToDirection(rayAngle);
			RaycastHit hit;
			bool didHit = Physics.Raycast(transform.position, rayDir, out hit, maxDistance: MaxDistance, layerMask: RayCastMask);
			if (didHit)
			{
				hitPoints.Add(transform.InverseTransformPoint(hit.point + MaskCutawayDistance * rayDir));
			}
			else
			{
				hitPoints.Add(transform.InverseTransformPoint((transform.position + rayDir * MaxDistance)));
			}
		}

		//Draw flashlight mesh:
		int vertexCount = hitPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];
		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = hitPoints[i];

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		_viewMesh.Clear();

		_viewMesh.vertices = vertices;
		_viewMesh.triangles = triangles;
		_viewMesh.RecalculateNormals();
	}

	public Vector3 AngleToDirection(float worldAngle)
	{
		return new Vector3(Mathf.Cos(worldAngle*Mathf.Deg2Rad), Mathf.Sin(worldAngle * Mathf.Deg2Rad), 0);
	} 

	private void DetectRobbers()
	{
		VisibleRobbers.Clear();
		foreach (var robber in DummyRobber.All)
		{
			Vector3 guardToRobber = (robber.transform.position - transform.position);
			float minCos = Mathf.Cos(Angle * Mathf.Deg2Rad / 2);
			bool withinFOV = Vector3.Dot(guardToRobber.normalized, GetViewDirection()) >= minCos;
			RaycastHit hit;
			bool obstacleInWay = Physics.Raycast(transform.position, guardToRobber.normalized, out hit, MaxDistance, RayCastMask);
			bool withinDistance = guardToRobber.magnitude <= MaxDistance;
			if (withinFOV && !obstacleInWay && withinDistance)
			{
				VisibleRobbers.Add(robber);
				Debug.DrawLine(robber.transform.position, robber.transform.position + 2 * Vector3.up, Color.red);
			}
		}
	}

	private Vector3 GetViewDirection()
	{
		return transform.up;
	}

	private float GetViewDirectionAngle()
	{
		return Mathf.Atan2(GetViewDirection().y, GetViewDirection().x) * Mathf.Rad2Deg; ;
	}
}
