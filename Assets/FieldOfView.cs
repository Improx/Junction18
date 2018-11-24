using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float Angle = 75;
	public float MaxDistance = 5;
	public float Resolution = 100;
	public MeshFilter ViewMeshFilter;

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
	}

	private void DrawFieldOfView()
	{
		Vector3 viewDir = transform.up;
		float viewDirectionAngle = Mathf.Atan2(viewDir.y, viewDir.x) * Mathf.Rad2Deg;
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
			bool didHit = Physics.Raycast(transform.position, rayDir, out hit);
			if (didHit)
			{
				hitPoints.Add(transform.InverseTransformPoint(hit.point));
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

		//Vector3[] vertices = new Vector3[3];
		//int[] triangles= new int[3];
		//vertices[0] = Vector3.zero;
		//vertices[1] = new Vector3(5, 0, 0);
		//vertices[2] = new Vector3(0, 5, 0);
		//triangles[0] = 0;
		//triangles[1] = 1;
		//triangles[2] = 2;

		_viewMesh.vertices = vertices;
		_viewMesh.triangles = triangles;
		_viewMesh.RecalculateNormals();
	}

	public Vector3 AngleToDirection(float worldAngle)
	{
		return new Vector3(Mathf.Cos(worldAngle*Mathf.Deg2Rad), Mathf.Sin(worldAngle * Mathf.Deg2Rad), 0);
	} 


}
