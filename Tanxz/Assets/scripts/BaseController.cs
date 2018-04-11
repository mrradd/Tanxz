using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* BaseController */
/**
* Base class for controlling the Tank.
******************************************************************************/
[RequireComponent(typeof(TankData), typeof(TankMotor), typeof(FiringMechanism))]
public abstract class BaseController : MonoBehaviour
  {
  /** Tank moving Audio Source. */
  public AudioSource tankMovingSound;

  /** Firing Mechanism. */
  public FiringMechanism firingMechanism;

  /** Tank control. */
  public TankMotor tankMotor;

  /** Tank data. */
  public TankData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
  protected virtual void Start()
    {
    /** Instantiate TankData. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();

    /** Instantiate Tank. */
    if(tankMotor == null)
      tankMotor = gameObject.GetComponent<TankMotor>();

    /** Instantiate FiringMechanism. */
    if(firingMechanism == null)
      firingMechanism = gameObject.GetComponent<FiringMechanism>();
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * playTankMovingSound */
  /**
  * Plays the tank moving sound if it is not playing.
  ****************************************************************************/
  public virtual void playTankMovingSound()
    {
    if(!tankMovingSound.isPlaying)
      tankMovingSound.Play();
    }

  /****************************************************************************
  * stopTankMovingSound */
  /**
  * Stops the tank moving sound if it is not playing.
  ****************************************************************************/
  public virtual void stopTankMovingSound()
    {
    if(tankMovingSound.isPlaying)
      tankMovingSound.Stop();
    }
  }
