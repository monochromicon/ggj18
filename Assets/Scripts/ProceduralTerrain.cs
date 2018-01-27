using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour {

	public int width = 10;
	public float spacing = 1f;
	public float maxHeight = 3f;
	public MeshFilter terrainMesh;

	// Use this for initialization
	void Start () {
		if (terrainMesh == null) {
			Debug.LogError("ProcuduralTerrain requires its target terrain Mesh to be assigned");
		}
		GenerateMesh();
	}

	float GetHeight (float x_coor, float z_coor) {
		float y_coor = Mathf.Min(0, maxHeight - Vector2.Distance(Vector2.zero, new Vector2(x_coor, z_coor)));
		return y_coor;
	}

	void GenerateMesh () {
		float startTime = Time.time;

		List<Vector3[]> verts = new List<Vector3[]>();
		List<int> tris = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		for (int z = 0; z < width; z++) {
			verts.Add(new Vector3[width]);
			for (int x = 0; x < width; x++) {
				Vector3 currentPoint = new Vector3();
				currentPoint.x = x * spacing;
				currentPoint.z = z * spacing;
				currentPoint.y = GetHeight(currentPoint.x, currentPoint.z);

				verts[z][x] = currentPoint;

				uvs.Add(new Vector2(x, z));
			}
		}

		tris.Add(0);
		tris.Add(1);
		tris.Add(width);

		Vector3[] unfoldedVerts = new Vector3[width*width];
		int i = 0;
		foreach (Vector3[] v in verts) {
			v.CopyTo(unfoldedVerts, i * width);
			i++;
		}

		Mesh ret = new Mesh();
		ret.vertices = unfoldedVerts;
		ret.triangles = tris.ToArray();
		ret.uv = uvs.ToArray();

		ret.RecalculateBounds();
		ret.RecalculateNormals();
		terrainMesh.mesh = ret;

		float diff = Time.time - startTime;
		Debug.Log("ProceduralTerrain was generated in " + diff + " seconds.");
	}
}
