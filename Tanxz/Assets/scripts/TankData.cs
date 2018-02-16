using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankData */
/**
* Holds all of the Tank's vital data.
******************************************************************************/
public class TankData : MonoBehaviour
  {
  /** Cannon launch force. */            public float cannonForce = 5000f;
  /** Delay between shots in seconds. */ public float firingDelay = .5f;
  /** Hit points. */                     public int   hp          = 100;
  /** Meters/second. */                  public float moveSpeed   = 4f;
  /** Shell damage. */                   public int   shellDamage = 34;
  /** Degrees/second. */                 public float turnSpeed   = 180f;
  }
