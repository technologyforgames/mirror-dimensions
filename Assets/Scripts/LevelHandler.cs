using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {


    public static LevelHandler instance;
    public RawImage overlay;
    public float fadeOutTime = 2f;
    public float fadeInTime = 2f;

    public static bool Fading {
        get { return instance.isFading; }
    }

    private Color startColor;
    private float fadeProgress;
    private bool isFading;
    private int levelIndex;


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
        startColor = overlay.color;
        StartCoroutine(ManualFade());
    }


    public void LoadNextLevel() {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
       // StartCoroutine(ManualFade(Color.clear, startcolor));

        if (sceneToLoad < SceneManager.sceneCountInBuildSettings) {
            {
                LoadLevel(sceneToLoad);
                //SceneManager.LoadScene(sceneToLoad);
            }
        } else {
            Debug.LogWarning("NO NEXT SCENE DEFINED");
        }
    }


    private static void LoadLevel(int aLevelIndex) {
        if (Fading) return;
        instance.levelIndex = aLevelIndex;
        instance.StartFade();
    }


    private void StartFade() {
        isFading = true;
        StartCoroutine(Fade());
    }


    private IEnumerator ManualFade() {
        Color fromColor = startColor;
        Color toColor = Color.clear;
        float fadeRate = 1f / 2f;
        fadeProgress = 0f;

        while (fadeProgress < 1f) {
            overlay.color = Color.Lerp(fromColor, toColor, fadeProgress);

            fadeProgress += fadeRate * Time.deltaTime;

            yield return null;
        }

        overlay.color = toColor;
    }


    private IEnumerator Fade() {
        fadeProgress = 0f;

        while (fadeProgress < 1.0f) {
            yield return new WaitForEndOfFrame();
            fadeProgress = Mathf.Clamp01(fadeProgress + Time.deltaTime / fadeOutTime);
            overlay.color = Color.Lerp(Color.clear, startColor, fadeProgress);
        }

        SceneManager.LoadScene(levelIndex);

        while (fadeProgress > 0f) {
            yield return new WaitForEndOfFrame();
            fadeProgress = Mathf.Clamp01(fadeProgress - Time.deltaTime / fadeInTime);
            overlay.color = Color.Lerp(Color.clear, startColor, fadeProgress);
        }

        isFading = false;
    }
}