using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour { 


    public static LevelHandler instance;
    public RawImage overlay;
    public float fadeTime;


    private Color startColor;
    private float fadeRate;
    private float fadeProgress;

    private void Awake() {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }


    private void Start() {
        // Fade out overlay
        startColor = overlay.color;
        StartCoroutine(FadeToClear());
    }


    public void LoadNextLevel() {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        if (sceneToLoad < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            Debug.LogWarning("NO NEXT SCENE DEFINED");
        }

        StartCoroutine(FadeToClear());
    }


    // Coroutine to fade out UI
    private IEnumerator FadeToClear() {
        overlay.gameObject.SetActive(true);
        fadeRate = 1f / fadeTime;
        fadeProgress = 0f;

        while (fadeProgress < 1f) {
            overlay.color = Color.Lerp(startColor, Color.clear, fadeProgress);

            fadeProgress += fadeRate * Time.deltaTime;

            yield return null;
        }

        overlay.color = Color.clear;
        overlay.gameObject.SetActive(false);
    }


}
