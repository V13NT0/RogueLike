using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    
    public static levelManager instance;

    public float waitToRespawn;
    private Vector3 spawnPoint;

    public int coinCollected;

    public string levelToLoad;

    private void Awake() {
        
        instance = this;
    }

    void Start()
    {
        spawnPoint = playerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {

        StartCoroutine(RespawnCo());
    

    }

    IEnumerator RespawnCo()
    {
        playerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / uiController.instance.fadeSpeed));

        uiController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / uiController.instance.fadeSpeed) + .2f);

        playerController.instance.transform.position = spawnPoint;

        yield return new WaitForSeconds((1f / uiController.instance.fadeSpeed) + .2f);

        playerController.instance.gameObject.SetActive(true);
        

        uiController.instance.FadeFromBlack();

        //Despues de spawnear y de dar la vida al jugador
        //uiController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        audioManager.instance.PlayLevelVictory();

        playerController.instance.stopInput = true;

        SpriteRenderer sr = playerController.instance.GetComponent<SpriteRenderer>();
        sr.gameObject.SetActive(false);

        cameraController.instance.stopFollow = true;

        uiController.instance.levelCompletedText.SetActive(true);

        yield return new WaitForSeconds (1.5f);

        uiController.instance.FadeToBlack();

        yield return new WaitForSeconds ((1f / uiController.instance.fadeSpeed) + .25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", coinCollected);

        SceneManager.LoadScene(levelToLoad);
    }
}
