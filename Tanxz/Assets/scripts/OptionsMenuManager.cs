using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/******************************************************************************
* OptionsMenuManager */
/**
* Handles updating the Map Seed input, and Multiplayer toggle.
******************************************************************************/
public class OptionsMenuManager : MonoBehaviour
  {
  /** Main menu reference. */
  public GameObject mainMenu;

  /** Map Of The Day Toggle element. */
  public Toggle mapOfTheDayToggle;

  /** Map Seed input field. */
  public InputField mapSeedInput;

  /** Multiplayer Toggle element. */
  public Toggle multiplayerToggle;

  /** Music Slider. */
  public Slider musicSlider;

  /** SFX Slider. */
  public Slider sfxSlider;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
	void Update ()
    {
    System.Int32.TryParse(mapSeedInput.text, out GameManager.instance.mapSeed);
    GameManager.instance.multiPlayer = multiplayerToggle.isOn;
    GameManager.instance.mapOfTheDay = mapOfTheDayToggle.isOn;
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * loadSettings */
  /**
  * Loads the Options settings, and updates the UI.
  ****************************************************************************/
  public void loadSettings()
    {
    float sfxVolume;
    float musicVolume;

    GameManager.instance.loadSettings();

    GameManager.instance.masterAudioMixer.GetFloat("sfxVolume", out sfxVolume);
    GameManager.instance.masterAudioMixer.GetFloat("musicVolume", out musicVolume);

    mapOfTheDayToggle.isOn = GameManager.instance.mapOfTheDay;
    multiplayerToggle.isOn = GameManager.instance.multiPlayer;
    mapSeedInput.text      = GameManager.instance.mapSeed.ToString();
    sfxSlider.value        = sfxVolume;
    musicSlider.value      = musicVolume;
    }

  /****************************************************************************
  * showMainMenu */
  /**
  * Switches to main menu.
  ****************************************************************************/
  public void showMainMenu()
    {
    GameManager.instance.saveSettings();
    gameObject.SetActive(false);
    mainMenu.gameObject.SetActive(true);
    }
  }
