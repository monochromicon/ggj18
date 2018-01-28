using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public ParticleSystem digSystem;
    public ParticleSystem broadcastSystem;
    public AudioSource call;
    public AudioSource dig;
    public AudioSource move;
    public AudioSource get;

    private Rigidbody rb;
    private bool moving;
    static public bool isBroadcasting = false;
    static public bool isDigging = false;
    static public int collected = 0;
    private int lastCollected = 0;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {        
        if (!digSystem.isEmitting) { isDigging = false; }
        if (!broadcastSystem.isEmitting) { isBroadcasting = false; }

        if (collected != lastCollected) {
            get.Play();
            lastCollected = collected;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (!moving) {
                move.Play();
                moving = true;
            }
        } 
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            move.Stop();
            moving = false;
        }
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        Vector3 movement = new Vector2 (moveHorizontal, 0.0f);

        rb.AddForce (movement * speed);
    }

}