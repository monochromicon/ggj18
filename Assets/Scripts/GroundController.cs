using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	public int width;
	public Material platformMaterial;
	
	private Vector3 lastJoint;

	// Use this for initialization
	void Start () {
		for (int i = -width; i < width; i++) {
			GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			GameObject parent = new GameObject("LeadingJoint");
			GameObject child = new GameObject("ProceedingJoint");

			parent.transform.SetParent(transform);
			platform.transform.SetParent(parent.transform);
			child.transform.SetParent(platform.transform);

			MeshRenderer platformMesh = platform.GetComponent<MeshRenderer>();
			platformMesh.material = platformMaterial;

			CapsuleCollider platformCollider = platform.GetComponent<CapsuleCollider>();
			platformCollider.radius = 0.25f;
			platform.transform.localScale = new Vector3(0.5f, 1, 1);
			
			platform.transform.localPosition -= new Vector3(0f, 0.75f, 0f);
			child.transform.localPosition -= new Vector3(0f, 0.75f, 0f);

			if (lastJoint == Vector3.zero) {
				parent.transform.position = new Vector3(i, 0f, 0f);
				parent.transform.rotation = Quaternion.Euler(0, 0, 90);
				lastJoint = child.transform.position;
			}
			else {
				parent.transform.position = lastJoint;
				parent.transform.rotation = Quaternion.Euler(0, 0, Random.Range(45, 135));
				lastJoint = child.transform.position;
			}
		}
	}
}
