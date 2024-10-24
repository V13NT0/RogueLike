using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapHolders : MonoBehaviour
{
    public mapHolders up, right, down, left;
    public bool isLevel, isLocked;

    public string levelToLoad, levelToCheck, levelName;

    public int coinsCollected, totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {
            if(PlayerPrefs.HasKey(levelToLoad + "_coins"))
            {
                coinsCollected = PlayerPrefs.GetInt(levelToLoad + "_coins");
            }

            isLocked = true;

            if(levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }
            
            if(levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
