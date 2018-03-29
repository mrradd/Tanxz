using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankData */
/**
* Holds all of the Tank's vital data.
******************************************************************************/
public class TankData : BaseData
  {
  /** Cannon launch force. */
  public float cannonForce = 5000f;

  /** Lowest amount of starting health. */
  public int baseHP = 100;

  /** Field of view angle. */
  public float fieldOfView = 45f;

  /** Delay between shots in seconds. */
  public float firingDelay = .5f;

  /** God Mode. */
  public bool godMode = false;

  /** Hit points. */
  public int hp = 100;

  /** Listening radius. */
  public float listeningRadius = 50f;

  /** Meters/second. */
  public float moveSpeed = 4f;

  /** Point value for destroying tank. */
  public int pointValue = 100;

  /** Remaining lives. */
  public int remainingLives = 3;

  /** Tank's score. */
  public int score = 0;

  /** Shell damage. */
  public int shellDamage = 34;

  /** Tank's name. */
  public string tankName = "";

  /** Degrees/second. */
  public float turnSpeed = 180f;

  /** Range can see. */
  public float viewDistance = 100f;

  /****************************************************************************
  * Sounds
  ****************************************************************************/
  public AudioSource audioSource;
  public AudioClip   soundFiring;
  public AudioClip   soundTankHit;
  public AudioClip   soundTankDestroyed;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Awake */
  /**
  ****************************************************************************/
  private void Awake()
    {
    hp = baseHP;

    /** Get the audio source. */
    if(audioSource == null)
      audioSource = gameObject.GetComponent<AudioSource>();
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  private void Update()
    {

    }
  }
