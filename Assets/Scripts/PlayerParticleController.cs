using UnityEngine;
using System.Collections;

public class PlayerParticleController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    void Start () {
        // Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate () {
        transform.position = player.transform.position + offset;
    }
}