using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Stalker */
/**
* AI that knows the player's position, and follows the player. It attacks when
* the Player is in sight.
******************************************************************************/
public class Stalker : AIPersonality
  {
  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
	protected override void Start ()
    {
		
	  }
	
	protected override void Update ()
    {
		
   	}

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * checkState */
  /**
  * Checks the state of the AI, and performs actions based on the current
  * state.
  ****************************************************************************/
  protected override void checkState()
    {
    /** Check movement mode. */
    switch(aiState)
      {
      /** Move along the waypoints list. */
      case AIState.Patrol:
        {
        patrol();
        break;
        }

      /** Chase the target. */
      case AIState.Chase:
        {
        chase();
        break;
        }

      /** Chase and attack player. */
      case AIState.ChaseAndFire:
        {
        break;
        }

      default:
        {
        chase();
        break;
        }
      }
    }
  }
