using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
	private float initialY;

    void Start () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z - player.transform.position.z);        
		// Get the initial Y of the camera
		initialY = transform.position.y;
        // Calculate and store the offset value by getting the distance between the player's position and camera's position.
        // offset =  new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate () {
		Vector3 newLoc = player.transform.position + offset;
		// Set new camera to follow only on X and Z
        // transform.position = new Vector3(newLoc.x, initialY, newLoc.z);
        transform.position = newLoc;
    }
}