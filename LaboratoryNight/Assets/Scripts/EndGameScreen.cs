using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour {

    public RawImage FadeImg;
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = false;
    private bool sceneEnding = false;
    private bool sceneStarted = false;

    void Awake()
    {
        FadeImg.uvRect = new Rect(0, 0, Screen.width, Screen.height);
        FadeImg.SetNativeSize();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !sceneStarted)
        {
            sceneStarting = true;
        }
        // If the scene is starting...
        if (sceneStarting)
        {
            StartScene();
        }

        if (sceneEnding)
        {
            FadeToBlack();
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);

        if (FadeImg.color.a >= 0.95f)
            // ... reload the level
            Application.LoadLevel(Application.loadedLevel);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
            sceneStarted = true;
        }
    }


    public void EndScene(int SceneNumber)
    {
        // Make sure the RawImage is enabled.
        sceneEnding = true;
        FadeImg.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (FadeImg.color.a >= 0.95f)
            // ... reload the level
            Application.LoadLevel(SceneNumber);
    }

}
