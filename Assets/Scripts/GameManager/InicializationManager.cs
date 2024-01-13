using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicializationManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Application.targetFrameRate = -1;
    }
}
