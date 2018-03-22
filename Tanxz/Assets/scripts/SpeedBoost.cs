using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* SpeedBoost */
/**
* Boosts speed property for a limited amount of time.
******************************************************************************/
public class SpeedBoost : Powerup
  {
  public float speedModifier;

  /****************************************************************************
  * onActivate */
  /**
  * Applies modifiers to the tank.
  ****************************************************************************/
  public override void onActivate(TankData target)
    {
    mTimeCounter = duration;

    target.moveSpeed += speedModifier;
    }

  /****************************************************************************
  * onDeactivate */
  /**
  * Reverses modifiers to tank.
  ****************************************************************************/
  public override void onDeactivate(TankData target)
    {
    target.moveSpeed -= speedModifier;
    }
  }
