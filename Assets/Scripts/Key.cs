using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // When player enters trigger, hide key and check which key was triggered to destroy correct wall
    private void OnTriggerEnter2D(Collider2D col) {
        this.gameObject.GetComponent<Renderer>().enabled = false;

        if (this.gameObject.tag == "BlueKey") {
            Fade(GameObject.FindWithTag("BlueWall"));
        }
        if (this.gameObject.tag == "YellowKey") {
            Fade(GameObject.FindWithTag("YellowWall"));
        }
        if (this.gameObject.tag == "RedKey") {
            Fade(GameObject.FindWithTag("RedWall"));
        }
    }

    // Fade out gameObject
    private void Fade(GameObject gameObject) {
        iTween.FadeTo(gameObject, iTween.Hash(  "alpha", 0f, 
                                                "onCompleteTarget", this.gameObject, 
                                                "onComplete", "DestroyAfterFade",
                                                "onCompleteParams", gameObject));
    }


    // Destroy gameObject
    private void DestroyAfterFade(GameObject gameObject) {
        Destroy(gameObject);
    }

}
