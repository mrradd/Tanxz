﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Powerup */
/**
* Base class for Poweups. Modifies TankData properties.
******************************************************************************/
[System.Serializable]
public class Powerup
  {
  protected float mTimeCounter;

  public float speedModifier;
  public int   healthModifier;
  public float fireRateModifier;

  public float duration;
  public bool  isPermanent;

  /****************************************************************************
  * onActivate */
  /**
  * Applies modifiers to the tank.
  ****************************************************************************/
  public virtual void onActivate(TankData target)
    {
    mTimeCounter = duration;

    target.moveSpeed += speedModifier;
    target.hp += healthModifier;
    target.firingDelay -= fireRateModifier;
    }

  /****************************************************************************
  * onDeactivate */
  /**
  * Reverses modifiers to tank.
  ****************************************************************************/
  public virtual void onDeactivate(TankData target)
    {
    target.moveSpeed -= speedModifier;
    target.firingDelay += fireRateModifier;
    }

  /****************************************************************************
  * updateTimer */
  /**
  * Updates the timer.
  ****************************************************************************/
  public virtual bool updateTimer()
    {
    mTimeCounter -= Time.deltaTime;

    return mTimeCounter <= 0f;
    }
  }
