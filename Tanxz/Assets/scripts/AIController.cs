using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AIController */
/**
* Manages controlling AI tanks.
******************************************************************************/
public class AIController : BaseController
  {

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
	protected override void Start()
    {
    base.Start();
	  }
	
	void Update()
    {
    /** Keep trying to fire a shell. Limited by rate of fire. */
    firingMechanism.fire(tankData.cannonForce);
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  }
