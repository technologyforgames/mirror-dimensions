using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player") {
            LevelHandler.instance.LoadEnding();
        }
    }
}
