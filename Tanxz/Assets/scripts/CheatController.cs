using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* CheatController */
/**
* Applies powerups to the tank.
******************************************************************************/
public class CheatController : MonoBehaviour
  {
  public PowerupController powerupController;
  public Powerup cheatPowerup;

  void Start()
    {
    if(powerupController == null)
      {
      powerupController = gameObject.GetComponent<PowerupController>();
      }
    }

	void Update ()
    {
    if(Input.GetKeyDown(KeyCode.P))
      {
      powerupController.addPowerup(cheatPowerup);
      }
	  }
  }
