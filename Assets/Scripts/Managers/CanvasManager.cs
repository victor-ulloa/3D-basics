using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Menu")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject creditsMenu;

    [Header("Button")]
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    [Header("Text")]
    [SerializeField] Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Buttons

        if (startButton) {
            startButton.onClick.AddListener(() => StartGame());
        }

        if (quitButton) {
            quitButton.onClick.AddListener(() => QuitGame());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame() {
        SceneManager.LoadScene("Level");
    }

    void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
