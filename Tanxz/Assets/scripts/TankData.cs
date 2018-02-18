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
  /** Cannon launch force. */             public float  cannonForce = 5000f;
  /** Delay between shots in seconds. */  public float  firingDelay = .5f;
  /** Hit points. */                      public int    hp          = 100;
  /** Meters/second. */                   public float  moveSpeed   = 4f;
  /** Tank's score. */                    public int    score       = 0;
  /** Shell damage. */                    public int    shellDamage = 34;
  /** Tank's name. */                     public string tankName    = "";
  /** Degrees/second. */                  public float  turnSpeed   = 180f;
  /** Point value for destroying tank. */ public int    pointValue  = 100;
  }
