using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* BaseController */
/**
* Base class for controlling the Tank.
******************************************************************************/
[RequireComponent(typeof(TankData), typeof(Tank))]
public abstract class BaseController : MonoBehaviour
  {
  /** Tank data. */    public TankData tankData;
  /** Tank control. */ public Tank     tank;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  protected virtual void Start()
    {
    /** Make sure we instantiate Tank Data if we don't have one. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();

    /** Make sure we instantiate Tank Motor if we don't have one. */
    if(tank == null)
      tank = gameObject.GetComponent<Tank>();
    }
  }
