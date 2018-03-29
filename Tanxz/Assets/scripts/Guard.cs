using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Guard */
/**
* Patrols and attacks/chases players. Retreats when below 50% health. Will go
* for pickups.
******************************************************************************/
public class Guard : AIPersonality
  {
  /****************************************************************************
  * Methods 
  ****************************************************************************/
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
  * checkState */
  /**
  * Checks the state of the AI, and performs actions based on the current
  * state.
  ****************************************************************************/
  protected override void checkState()
    {
    /** Flee if lower than 50% health and facing a Player. */
    if(tankData.hp <= tankData.baseHP / 2 && targetIsPlayer())
      aiState = AIState.Flee;

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
      case AIState.Flee:
        {
        flee();
        break;
        }

      /** Chase and attack player. */
      case AIState.ChaseAndFire:
        {
        chaseAndFire();
        break;
        }

      /** Chase target. */
      case AIState.Chase:
        {
        chase();
        break;
        }
      }
    }

  /****************************************************************************
  * heardTarget */
  /**
  * AI Listener calls this when a target is found.
  * 
  * @param  tf  Transform of new target.
  ****************************************************************************/
  public override void heardTarget(Transform tf)
    {
    if(target == null)
      {
      target = tf;
      }
    }

  /****************************************************************************
  * targetIsPlayer */
  /**
  * Verifies if i.gameObjects a player.
  * 
  * @returns  True: alive player target. False: not an alive player target.
  ****************************************************************************/
  protected bool targetIsPlayer()
    {
    if(target != null && target.gameObject.GetComponent<BaseData>().isAlive)
      {
      for(int i = 0; i < GameManager.instance.playerTanks.Count; i++)
        if(target.gameObject == GameManager.instance.playerTanks[i])
          return true;
      }

    return false; 
    }

  /****************************************************************************
  * scenserTarget */
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
        target = aiSight.scanForType(AISight.TargetType.Pickup);
      }

    /** Found a Player, set to Chase and Fire. */
    if(targetIsPlayer())
      aiState = AIState.ChaseAndFire;

    /** Found a target, set to Chase. */
    else if(hasActiveTarget())
      aiState = AIState.Chase;

    /** Return to Patrol. */
    else
      aiState = AIState.Patrol;
    }
  }
