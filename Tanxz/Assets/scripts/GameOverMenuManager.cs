using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/******************************************************************************
* GameOverMenuManager */
/**
* Manages the Game Over Menu.
******************************************************************************/
public class GameOverMenuManager : MonoBehaviour
  {
  public Text highScoreText;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  void Update()
    {
    /** Change the high score text. */
    highScoreText.text = "High Score: " + GameManager.instance.highScore;
    }
  }
