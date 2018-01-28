using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {

    public bool cinematicMode = true;
    public GameObject player;
    public Text titleText;
    public float fadeSpeed;
    private Vector3 offset;
	private float initialY;

    void Start () {
        if (cinematicMode) {
            StartCoroutine(Title());
        }
        // else {
        //     transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z - player.transform.position.z);
        //     offset = transform.position - player.transform.position;
        // }
    }
    
    // void Update () {
    //     if (!cinematicMode && (titleText.color.a > 0f)) {
    //         titleText.color.a -= Time.time * speed;
    //     }
    // }

    void LateUpdate () {
		Vector3 newLoc = player.transform.position + offset;
		// Set new camera to follow only on X and Z
        // transform.position = new Vector3(newLoc.x, initialY, newLoc.z);
        transform.position = newLoc;
    }

    IEnumerator Title () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, transform.position.z - player.transform.position.z);            
        offset = transform.position - player.transform.position;
        yield return new WaitForSeconds(2);
        cinematicMode = false;
        StartCoroutine(FadeTextToZeroAlpha(1f, titleText));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}