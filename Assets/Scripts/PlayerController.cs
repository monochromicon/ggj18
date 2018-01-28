using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public ParticleSystem digSystem;
    public ParticleSystem broadcastSystem;
    public AudioSource call;
    public AudioSource dig;
    public AudioSource move;

    private Rigidbody rb;
    private bool isMoving = false;
    private bool isBroadcasting = false;
    private bool isDigging = false;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {        
        if (!digSystem.isEmitting) { isDigging = false; }
        if (!broadcastSystem.isEmitting) { isBroadcasting = false; }

        if (!isBroadcasting) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                broadcastSystem.Play();
                call.Play();
                isBroadcasting = true;
            }
        }

        if (!isDigging) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                digSystem.Play();
                dig.Play();
                isDigging = true;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            move.Play();
        } else {
            move.Stop();
        }
    }

    public void Dig () {

    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        Vector3 movement = new Vector2 (moveHorizontal, 0.0f);

        rb.AddForce (movement * speed);
    }

    // void OnCollisionExit () {
    //     falling = true;
    // }

    // void OnCollisionEnter () {
    //     falling = false;
    // }
}