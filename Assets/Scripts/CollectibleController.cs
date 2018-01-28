using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleController : MonoBehaviour {

	private GameObject player;
	private ParticleSystem collectibleSystem;
	private bool collected;
	private float pitch;
	private AudioSource response;
	private AudioClip responseClip;

	// Use this for initialization
	void Start () {
		pitch = Random.Range(0.5f, 3f);
		player = GameObject.Find("Player");
		collectibleSystem = GetComponentInChildren<ParticleSystem>();
		response = GetComponent<AudioSource>();
		response.pitch = pitch;
	}
	
	// Update is called once per frame
	void Update () {
		if (!collected && PlayerController.isBroadcasting) {
			StartCoroutine(Respond());
		}
	}

	void OnCollisionStay(Collision collision) {
		if (!collected && PlayerController.isDigging && collision.collider == player.GetComponent<SphereCollider>()) {
			Debug.Log("Collected!");
			collectibleSystem.Play();
			Text score = GameObject.Find("Canvas/Score").GetComponent<Text>();
			score.text = (PlayerController.collected + 1).ToString();
			PlayerController.collected += 1;
			collected = true;
		}
	}

	IEnumerator Respond () {
		yield return new WaitForSeconds(1);
		response.Play();
	}
}
