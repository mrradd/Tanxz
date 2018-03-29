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
  public GameObject mainMenu;
  public InputField mapSeedInput;
  public Toggle     multiplayerToggle;
  public Toggle     mapOfTheDayToggle;
	
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
  * showMainMenu */
  /**
  * Switches to main menu.
  ****************************************************************************/
  public void showMainMenu()
    {
    gameObject.SetActive(false);
    mainMenu.gameObject.SetActive(true);
    }
  }
