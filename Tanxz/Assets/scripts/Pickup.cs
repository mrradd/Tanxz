using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* PickupSpawner */
/**
* Gives tank a powerup when collided with.
******************************************************************************/
public class Pickup : BaseData
  {
  /** Start position for the Pickup. */
  protected Vector3 mStartPosition;

  /** Counter for respawn delay. */
  protected float mRespawnTimer;

  /** Audio feedback. */
  public AudioClip feedback;

  /** Powerup to apply. */
  public Powerup powerup;

  /** Amount of time before respawn. */
  public float respawnTime;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Awake */
  /**
  ****************************************************************************/
  void Awake()
    {
    mStartPosition = gameObject.transform.position;
    isAlive = true;
    }

  /****************************************************************************
  * OnTriggerEnter */
  /**
  ****************************************************************************/
  void OnTriggerEnter(Collider other)
    {
    /** Powerup Controller of Collider object. */
    PowerupController pc = other.GetComponent<PowerupController>();

    /** Add the powerup if there is a Powerup Controller. */
    if(pc != null)
      {
      /** Play feedback audio. */
      AudioSource.PlayClipAtPoint(feedback, gameObject.transform.position);

      /** Add the powerup. */
      pc.addPowerup(powerup);

      /** Set hidden. */
      isAlive = false;

      /** Deactivate spawner. */
      gameObject.transform.position = new Vector3(1000, 1000, 1000);
      }
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  void Update()
    {
    if(!isAlive)
      mRespawnTimer -= Time.deltaTime;

    if(mRespawnTimer <= 0)
      {
      isAlive = true;
      gameObject.transform.position = mStartPosition;
      mRespawnTimer = respawnTime;
      }
    }
  }
