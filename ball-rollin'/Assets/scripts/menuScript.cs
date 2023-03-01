using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuScript : MonoBehaviour
{
    [SerializeField] private GameObject restartText;
    [SerializeField] private GameObject resumeText;
    public GameObject menu;

    void Start()
    {
        menu.SetActive(false);
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        menu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }

    }
}

