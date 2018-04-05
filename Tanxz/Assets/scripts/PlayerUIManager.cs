using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/******************************************************************************
* PlayerUIManager */
/**
* Manages updating the Player UI.
******************************************************************************/
public class PlayerUIManager : MonoBehaviour
  {
  public TankData tankData;
  public Text txtHP;
  public Text txtScore;
  public Text txtLives;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
	void Start ()
    {
		
	  }
	
  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
	void Update ()
    {
    updateLives();
    updateScore();
    updateHP();
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * updateLives */
  /**
  * Updates the Lives Text element.
  ****************************************************************************/
  protected void updateLives()
    {
    txtLives.text = "Lives: " + tankData.remainingLives;
    }

  /****************************************************************************
  * updateScore */
  /**
  * Updates the Score Text element.
  ****************************************************************************/
  protected void updateScore()
    {
    txtScore.text = "Score: " + tankData.score;
    }

  /****************************************************************************
  * updateHP */
  /**
  * Updates the HP Text element.
  ****************************************************************************/
  protected void updateHP()
    {
    txtHP.text = "HP: " + tankData.hp;
    }
  }
