using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {

    private bool playerEntered = false;
    private bool mirrorPlayerEntered = false;

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player") {
            Debug.Log("Player entered");
            playerEntered = true;
        }
        if (target.tag == "MirrorPlayer") {
            Debug.Log("Mirror player entered");
            mirrorPlayerEntered = true;
        }
    }

    private void Update() {
        if (playerEntered && mirrorPlayerEntered) {
            SceneManager.LoadScene("Main");
}
    }
}
