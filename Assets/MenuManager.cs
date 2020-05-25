using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    public void startGame()
    {
        PlayerPrefs.SetString("sahne", "new");// bu bilgiyi playerdan alacağım
        SceneManager.LoadScene(0);
    }
    public void Load()
    {
        PlayerPrefs.SetString("sahne", "load");
        SceneManager.LoadScene(0);

    }
}
