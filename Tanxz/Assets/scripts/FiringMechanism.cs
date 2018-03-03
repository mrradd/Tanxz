﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* FiringMechanism */
/**
* Manages firing the Cannon.
******************************************************************************/
[RequireComponent(typeof(TankData), typeof(Cannon))]
public class FiringMechanism : MonoBehaviour
  {
  /** Time until next shot. */
  protected float mShotTimer = 0f;

  /** Tank's Cannon. */
  public Cannon cannon;

  /** Tank's Data. */
  public TankData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  void Start()
    {
    /** Instantiate Cannon. */
    if(cannon == null)
      cannon = gameObject.GetComponentInChildren<Cannon>();

    /** Instantiate TankData. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();
    
    }

  void Update()
    {
    /** Update the shot timer. */
    mShotTimer += Time.deltaTime;
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * fire */
  /**
  * Fires a tank shell from the current .
  * 
  * @param  force  Force to fire cannon at.
  ****************************************************************************/
  public void fire(float force)
    {
    /** Fire a cannonball */
    if(mShotTimer >= tankData.firingDelay)
      {
      mShotTimer = 0f;
      cannon.fire(force, gameObject.name);
      }
    }
  }