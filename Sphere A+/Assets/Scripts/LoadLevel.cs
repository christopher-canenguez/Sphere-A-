using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    // Name of the scene you want to load.
    public string nameOfTheSceneToLoad = "";

    // Load the level using SceneManager.
    public void loadLevel()
    {
        SceneManager.LoadScene(nameOfTheSceneToLoad);
    } // End loadLevel.
} // End script.

