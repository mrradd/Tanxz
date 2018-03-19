using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Aggressive */
/**
* Patrols and attacks players and ai, and attacks when spotted. Does not go for
* pickups. Prefers hunting players.
******************************************************************************/
public class Aggressive : AIPersonality
  {
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

      /** Chase and attack player. */
      case AIState.ChaseAndFire:
        {
        chaseAndFire();
        break;
        }
      }
    }

  /****************************************************************************
  * chaseAndFire */
  /**
  * Chases the target while firing.
  ****************************************************************************/
  protected override void chaseAndFire()
    {
    base.chase();
    base.fire();
    }

  /****************************************************************************
  * scanForTarget */
  /**
  * Scans the area for AITanks and Player Tanks.
  ****************************************************************************/
  protected override void scanForTarget()
    {
    /** Search for target. */
    if(!hasActiveTarget())
      {
      target = aiSight.scanForType(AISight.TargetType.PlayerTank);

      if(target == null)
        target = aiSight.scanForType(AISight.TargetType.AITank);      
      }

    /** Set the AI State to Chase and Fire. */
    if(hasActiveTarget())
      {
      aiState = AIState.ChaseAndFire;
      }

    /** Remove target, and set state to Patrol. */
    else
      {
      target  = null;
      aiState = AIState.Patrol;
      }
    }
  }
