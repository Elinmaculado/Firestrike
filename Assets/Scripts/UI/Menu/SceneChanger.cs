using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}