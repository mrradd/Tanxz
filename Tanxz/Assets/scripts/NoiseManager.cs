using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* NoiseManager */
/**
* Manages noise property of tank.
******************************************************************************/
public class NoiseManager : MonoBehaviour
  {
  /** Noise counter. */
  protected float mNoiseTimeCounter = 0f;

  /** Noise counter. */
  protected float mNoiseTimer = .5f;

  /** Tank Data. */
  public BaseData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  private void Update()
    {
    /** Update noise counter. */
    if(mNoiseTimeCounter >= 0f)
      {
      mNoiseTimeCounter -= Time.deltaTime;

      /** Turn off making noise counter. */
      if(mNoiseTimeCounter <= 0f)
        tankData.makingNoise = false;
      }
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * madeNoise */
  /**
  * Call this method anytime the tank does something that is considered noisy.
  ****************************************************************************/
  public void madeNoise()
    {
    Debug.Log(gameObject.name + " made Noise");
    mNoiseTimeCounter    = mNoiseTimer;
    tankData.makingNoise = true;
    }
  }
