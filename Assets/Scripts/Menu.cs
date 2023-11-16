using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void Quit()
    {
        print("Salí");
        Application.Quit();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
