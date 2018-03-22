using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* PowerupController */
/**
* Manages picked up powerups.
******************************************************************************/
public class PowerupController : MonoBehaviour
  {
  /** List of temporary Powerups. */
  public List<Powerup> powerups = new List<Powerup>();

  /** TankData reference. */
  public TankData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  void Update()
    {
    List<Powerup> temp = new List<Powerup>();

    /** Update the Powerups. */
    foreach(Powerup power in powerups)
      {
      /** Update power up timer, and check to deactivate Powerup. */
      if(power.updateTimer())
        temp.Add(power);
      }

    /** Deactivate Powerups. */
    foreach(Powerup power in temp)
      {
      power.onDeactivate(tankData);
      powerups.Remove(power);
      }
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * addPowerup */
  /**
  * Adds a powerup to the powerup list and activates it. 
  * @param  powerup  Powerup to add/activate.
  ****************************************************************************/
  public void addPowerup(Powerup powerup)
    {
    powerup.onActivate(tankData);

    if(!powerup.isPermanent)
      {
      powerups.Add(powerup);
      }
    }
  }
