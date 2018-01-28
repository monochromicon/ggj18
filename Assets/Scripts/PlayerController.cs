using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public ParticleSystem digSystem;
    public ParticleSystem broadcastSystem;

    private Rigidbody rb;
    private bool isBroadcasting = false;
    private bool isDigging = false;
    private bool falling = true;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        if (!digSystem.isEmitting) { isDigging = false; }
        if (!broadcastSystem.isEmitting) { isBroadcasting = false; }

        if (!isBroadcasting) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                broadcastSystem.Play();
                isBroadcasting = true;
            }
        }

        if (!isDigging) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                digSystem.Play();
                isDigging = true;
            }
        }
    }

    public void Dig () {

    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        Vector3 movement = new Vector2 (moveHorizontal, 0.0f);

        // if (!falling) {
            rb.AddForce (movement * speed);
        // }
    }

    // void OnCollisionExit () {
    //     falling = true;
    // }

    // void OnCollisionEnter () {
    //     falling = false;
    // }
}