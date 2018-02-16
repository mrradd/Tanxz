﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankData */
/**
* Holds all of the Tank's vital data.
******************************************************************************/
public class TankData : MonoBehaviour
  {
  /** Cannon launch force. */ public float cannonForce = 200f;
  /** Meters/second. */       public float moveSpeed   = 3f;
  /** Degrees/second. */      public float turnSpeed   = 180f;
  }
