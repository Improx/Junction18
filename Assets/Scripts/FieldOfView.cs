using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float Angle = 75;
	public float MaxDistance = 5;
	public float Resolution = 100;
	public MeshFilter ViewMeshFilter;
	public MeshFilter ViewMeshFilterForRobbers;
	public LayerMask RayCastMask;
	public float MaskCutawayDistance = 0.18f;
	[HideInInspector] public List<Robber> VisibleRobbers = new List<Robber>();

	private Mesh _viewMesh;

	public delegate void DetectionEvent(Robber r, string status);
	public static event DetectionEvent Detected;

	// Use this for initialization
	void Start () {
		_viewMesh = new Mesh();
		_viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = _viewMesh;
		ViewMeshFilterForRobbers.mesh = _viewMesh;
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
		int numRays = Mathf.RoundToInt(Angle * Resolution);
		float angleIncrement = Angle / numRays;

		//Shoot rays and save hit points:
		List<Vector2> hitPoints = new List<Vector2>();
		for (int i = 0; i < numRays; i++)
		{
			float rayAngle = lowAngle + angleIncrement * i;
			Vector2 rayDir = AngleToDirection(rayAngle);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, distance: MaxDistance, layerMask: RayCastMask);
			if (hit.collider != null)
			{
				hitPoints.Add(transform.InverseTransformPoint(hit.point + MaskCutawayDistance * rayDir));
			}
			else
			{
				hitPoints.Add(transform.InverseTransformPoint(((Vector2)transform.position + rayDir * MaxDistance)));
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
		if (GetComponent<Player>().Team == PlayerType.Robber) return;

		foreach (var robber in VisibleRobbers)
		{
			//If player and guard:
			if (GetComponent<Player>().isLocalPlayer) robber.GetComponentInChildren<SpriteRenderer>().enabled = false;
			robber.FlashlightRadiance = 0;
		}
		VisibleRobbers.Clear();
		foreach (var robber in Robber.All)
		{
			Vector3 guardToRobber = (robber.transform.position - transform.position);
			float minCos = Mathf.Cos(Angle * Mathf.Deg2Rad / 2);
			float viewToRobberCos = Vector3.Dot(guardToRobber.normalized, GetViewDirection());
			bool withinFOV = viewToRobberCos >= minCos;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, guardToRobber.normalized, guardToRobber.magnitude, RayCastMask);
			bool obstacleInWay = hit.collider != null;
			bool withinDistance = guardToRobber.magnitude <= MaxDistance;
			if (withinFOV && !obstacleInWay && withinDistance)
			{
				if (Detected != null) Detected(robber, "Add");
				VisibleRobbers.Add(robber);
				robber.GetComponentInChildren<SpriteRenderer>().enabled = true;
				float distanceFlashContrib = 1 - Mathf.InverseLerp(1, MaxDistance, guardToRobber.magnitude);
				float angleFlashContrib = Mathf.InverseLerp(minCos, 1, viewToRobberCos);
				robber.FlashlightRadiance = distanceFlashContrib * angleFlashContrib;
				Debug.DrawLine(robber.transform.position, robber.transform.position + 2 * Vector3.up, Color.red);
			}
			else if (Detected != null) Detected(robber, "Remove");
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
