using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string startScene, continueScene;

    public GameObject continueBottom;

    private void Start() 
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueBottom.SetActive(true);
        } else 
        {
            continueBottom.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }
}
