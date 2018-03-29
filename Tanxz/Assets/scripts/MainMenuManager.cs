using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/******************************************************************************
* MainMenuManager */
/**
* Handles Main Menu operations.
******************************************************************************/
public class MainMenuManager : MonoBehaviour
  {
  public GameObject optionsMenu;

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * options */
  /**
  * Switches to Options screen.
  ****************************************************************************/
  public void showOptions()
    {
    gameObject.SetActive(false);
    optionsMenu.gameObject.SetActive(true);
    }

  /****************************************************************************
  * startGame */
  /**
  * Starts game.
  ****************************************************************************/
  public void startGame()
    {
    GameManager.instance.loadMainGameScene();
    }
  }
