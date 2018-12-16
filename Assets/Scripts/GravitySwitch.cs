using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite flippedSprite;

    internal bool isFlipped = false;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (isFlipped) {
            spriteRenderer.sprite = flippedSprite;
        } else {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public void ToggleSwitch() {
        isFlipped = !isFlipped;

        if (isFlipped) {
            spriteRenderer.sprite = flippedSprite;
        } else {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
