﻿using UnityEngine;
using System.Collections;

public class MirrorPlayer : PlayerController {

    private void Start()
    {
        // Reverse gravity
        rb2d.gravityScale *= -1;
        jumpForce *= -1;
    }
}