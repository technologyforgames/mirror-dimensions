using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Key was picked up");
        Destroy(this.gameObject);

        if (this.gameObject.tag == "BlueKey") {
            Destroy(GameObject.FindWithTag("BlueWall"));
        }
    }
}
