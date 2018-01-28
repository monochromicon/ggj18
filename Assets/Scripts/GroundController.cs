using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	public int width;
	public int angleRange = 45;
	public float spawnRate = 1;
	public Material platformMaterial;
	public ParticleSystem collectibleSystem;
	public AudioClip responseClip;
	public AudioClip getClip;
	public AudioClip proximityClip;
	public int collectibles;
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
				parent.transform.rotation = Quaternion.Euler(0, 0, Random.Range(90 - angleRange, 90 + angleRange));
				lastJoint = child.transform.position;
			}

			if (Random.value < spawnRate) {
				ParticleSystem collectible = Instantiate(collectibleSystem);
				collectible.transform.SetParent(platform.transform);
				collectible.transform.position = platform.transform.position;
				platform.AddComponent<CollectibleController>();
				AudioSource responseAudio = platform.AddComponent<AudioSource>();
				responseAudio.loop = false;
				responseAudio.playOnAwake = false;
				responseAudio.clip = responseClip;
				AudioSource proximityAudio = platform.AddComponent<AudioSource>();
				proximityAudio.clip = proximityClip;
				proximityAudio.spread = 1f;
				proximityAudio.spatialBlend = 1f;
				proximityAudio.rolloffMode = AudioRolloffMode.Logarithmic;
				proximityAudio.maxDistance = 20f;
				AudioSource getAudio = platform.AddComponent<AudioSource>();
				getAudio.clip = getClip;
				getAudio.playOnAwake = false;
				getAudio.loop = false;
				collectibles += 1;
			}
		}
	}
}
