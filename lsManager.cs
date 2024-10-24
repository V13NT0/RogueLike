using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lsManager : MonoBehaviour
{
    public lsPlayer player;

    private mapHolders[] allPoints;

    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<mapHolders>();

        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(mapHolders point in allPoints)
            {
                if(point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    player.transform.position = point.transform.position;
                    player.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCO());
    }

    public IEnumerator LoadLevelCO()
    {
        lsUiManager.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / lsUiManager.instance.fadeSpeed) + .25f);

        SceneManager.LoadScene(player.currentPoint.levelToLoad);
    }
}
