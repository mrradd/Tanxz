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

  /** Hit points. */
  [System.NonSerialized]
  public int hp = 100;

  /** Listening radius. */
  public float listeningRadius = 50f;

  /** Meters/second. */
  public float moveSpeed = 4f;

  /** Point value for destroying tank. */
  public int pointValue = 100;

  /** Range can see. */
  public float viewDistance = 100f;

  /** Tank's score. */
  public int score = 0;

  /** Shell damage. */
  public int shellDamage = 34;

  /** Tank's name. */
  public string tankName = "";

  /** Degrees/second. */
  public float turnSpeed = 180f;

  /**************************************************************************
  * Awake */
  /**
  **************************************************************************/
  private void Awake()
    {
    hp = baseHP;
    }

  /**************************************************************************
  * Update */
  /**
  **************************************************************************/
  private void Update()
    {
    /** If the health drops below 0, set as dead and change color. */
    if(hp <= 0 && isAlive)
      {
      isAlive = false;

      /** Get materials for all child components so we can change their color. */
      Renderer[] arr = gameObject.GetComponentsInChildren<Renderer>();

      /** Change all the components color. */
      for(int i = 0; i < arr.Length; i++)
        {
        if(arr[i] == null)
          continue;

        arr[i].material.color = Color.white;
        }
      }
    }
  }
