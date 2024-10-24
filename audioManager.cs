using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{

    public static audioManager instance;

    public AudioSource[] sound;

    public AudioSource bgm, endMusic;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySSFX(int soundToPlay)
    {
        sound[soundToPlay].Stop();

        sound[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        sound[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        endMusic.Play();
    }
}
