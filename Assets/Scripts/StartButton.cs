using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    public AudioSource backgroundMusic;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadIntro() {
        audioSource.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("Intro");
    }

}
