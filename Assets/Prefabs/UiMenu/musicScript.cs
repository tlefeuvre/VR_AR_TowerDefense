using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicScript : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject menuSound;
    public GameObject sliderMusic;
    public GameObject sliderSound;
    // Start is called before the first frame update
    void Start()
    {
       backgroundMusic.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("backgroundMusicVolume") / 25);
       menuSound.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("menuSoundVolume") / 15);
        
        sliderMusic.GetComponent<Slider>().value = PlayerPrefs.GetFloat("backgroundMusicVolume");
        sliderSound.GetComponent<Slider>().value = PlayerPrefs.GetFloat("menuSoundVolume");
    }

    // Update is called once per frame
    void Update()
    {
         PlayerPrefs.SetFloat("backgroundMusicVolume", sliderMusic.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("menuSoundVolume", sliderSound.GetComponent<Slider>().value);
        backgroundMusic.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("backgroundMusicVolume") / 25);
        menuSound.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("menuSoundVolume") / 10);
    }
}

