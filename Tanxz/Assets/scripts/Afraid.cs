using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Afraid */
/**
* Patrols and runs from everything. Does not get pickups.
******************************************************************************/
public class Afraid : AIPersonality
  {
  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /**************************************************************************
  * Awake */
  /**
  **************************************************************************/
  protected override void Awake()
    {
    base.Awake();
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  protected override void Start()
    {
    base.Start();
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  protected override void Update()
    {
    base.Update();
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

      /** Chase and attack player. */
      case AIState.Flee:
          {
          flee();
          break;
          }
      }
    }

  /****************************************************************************
  * scanForTarget */
  /**
  * Scans the area for AITanks and Player Tanks.
  ****************************************************************************/
  protected override void scanForTarget()
    {
    target = aiSight.scanForType(AISight.TargetType.PlayerTank);

    if(target == null)
      target = aiSight.scanForType(AISight.TargetType.AITank);

    /** Set the AI State to Flee. */
    if(target != null)
      {
      aiState = AIState.Flee;
      }
    }
  }
