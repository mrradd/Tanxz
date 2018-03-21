using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Powerup */
/**
* Base class for Poweups. Modifies TankData properties.
******************************************************************************/
public class Powerup
  {
  public float speedModifier;
  public int   healthModifier;
  public float fireRateModifier;

  public float duration;
  public bool  isPermanent;

  /****************************************************************************
  * OnActivate */
  /**
  * Applies modifiers to the tank.
  ****************************************************************************/
  public virtual void OnActivate(TankData target)
    {
    target.moveSpeed += speedModifier;
    target.hp += healthModifier;
    target.firingDelay -= fireRateModifier;
    }

  /****************************************************************************
  * OnDeactivate */
  /**
  * Reverses modifiers to tank.
  ****************************************************************************/
  public virtual void OnDeactivate(TankData target)
    {
    target.moveSpeed -= speedModifier;
    target.hp -= healthModifier;
    target.firingDelay += fireRateModifier;
    }
  }
