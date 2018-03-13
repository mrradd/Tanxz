﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AIPersonality */
/**
* Manages controlling AI tanks and AI behavior.
******************************************************************************/
public class AIPersonality : BaseController
  {
  /****************************************************************************
  * Enums
  ****************************************************************************/
  /** Loop type. */
  public enum LoopType { Stop, Loop, PingPong};

  /** Different AI states. */
  public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Patrol, Rest };

  /****************************************************************************
  * Properties
  ****************************************************************************/
  /** Current state of avoidance algorithm. */
  protected int mAvoidanceStage = 0;

  /** Timer before avoidance ends. */
  protected float mAvoidanceTimer = 0f;

  /** Index of current waypoints. */
  protected int mCurrentWaypointIndex = 0;

  /** Flee time tracker. */
  protected float mFleeTimer = 0f;

  /** Follow the waypoints forward. */
  protected bool mPatrolForward = true;

  /** Time state enterred. */
  protected float mTimeStateEnterred;

  /** Current AI mode. */
  public AIState aiState = AIState.Patrol;

  /** Time period move forward to avoid object. */
  public float avoidanceTime = 2.0f;

  /** Threshhold for being close enough to waypoint. */
  public float distanceThreshold = 1f;

  /** Amount of time spent fleeing. */
  public float fleeTime = 1f;

  /** Loop type. */
  public LoopType loopType;

  /** Sensor range. */
  public float senseRadius;

  /** Target to chase. */
  public Transform target;

  /** List of Waypoints. */
  public Transform[] waypoints;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  protected virtual void Awake()
    {
    }

  protected override void Start()
    {
    base.Start();
	  }
	
  protected virtual void Update()
    {
    /** Avoid obstacles. */
    if(mAvoidanceStage != 0)
      avoid();
    else
      {
      /** Move according to mode if we can. */
      if(canMove())
        {
        checkState();
        }

      /** Change avoidance stage. */
      else
        {
        mAvoidanceStage = 1;
        }
      }
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * targetSighted */
  /**
  * Spotted a target.
  * 
  * @param  obj  Game Object sighted.
  ****************************************************************************/
  public virtual void targetSighted(GameObject obj)
    {
    
    }

  /****************************************************************************
  * avoid */
  /**
  * Attempts to avoid an obstacle if one is encountered.
  ****************************************************************************/
  protected virtual void avoid()
    {
    /** Rotate the tank, and advance avoidance stage. */
    if(mAvoidanceStage == 1)
      {
      //TODO CH  TURN UNTIL PERPENDICULAR TO OBJECT
      /** Turn left. */
      tankMotor.rotate(-1 * tankData.turnSpeed);

      /** Change avoidance stage, and reset timer. */
      if(canMove())
        {
        mAvoidanceTimer = 0;
        mAvoidanceStage = 2;
        }
      }

    /** Move tank away from obstacle. */
    else if(mAvoidanceStage == 2)
      {
      if(canMove())
        {
        mAvoidanceTimer += Time.deltaTime;

        /** Move tank. */
        if(mAvoidanceTimer < avoidanceTime)
          {
          tankMotor.move(tankData.moveSpeed);
          }
        /** Reset Avoidance Stage. */
        else
          {
          mAvoidanceStage = 0;
          }
        }

      /** Change to stage 1 to attempt to rotate. */
      else
        {
        mAvoidanceStage = 1;
        }
      }

    /** Initiate first avoidance stage. */
    else
      {
      mAvoidanceStage = 1;
      }
    }

  /****************************************************************************
  * canMove */
  /**
  * Returns if the tank can move forward tank movement speed units ahead.
  * 
  * @returns True: can move forward. False: can't move forward.
  ****************************************************************************/
  protected bool canMove()
    {
    RaycastHit hit;

    /** Check if something is in front of the tank. */
    if(Physics.Raycast(tankMotor.tf.position, tankMotor.tf.forward, out hit, tankData.moveSpeed))
      {
      /** Avoid everything but the player. */
      if(!hit.collider.CompareTag("PlayerTank"))
        {
        return false;
        }
      }

    return true;
    }

  /****************************************************************************
  * chase */
  /**
  * Moves tank in such a way that it appears to be chasing the target.
  ****************************************************************************/
  protected virtual void chase()
    {
    if(target != null)
      {
      /** Square the difference of magnitudes, and check if it is within our
        * threshold squared. Doing this instead of using Vector3.Distance for
        * better performance. */
      float d = Vector3.SqrMagnitude(tankMotor.tf.position - target.position);
      float dt = Mathf.Pow(distanceThreshold, 2f);

      tankMotor.rotateTowards(target.position, tankData.turnSpeed);

      /** Move forward if far enough away. */
      if(d >= dt)
        {
        tankMotor.move(tankData.moveSpeed);
        }
      }
    else
      aiState = AIState.Patrol;  
    }

  /****************************************************************************
  * chaseAndFire */
  /**
  * Intentionally blank.
  ****************************************************************************/
  protected virtual void chaseAndFire() { }

  /****************************************************************************
  * checkForFlee */
  /**
  * Intentionally blank.
  ****************************************************************************/
  protected virtual void checkForFlee() { }

  /****************************************************************************
  * checkState */
  /**
  * Checks the state of the AI, and performs actions based on the current
  * state.
  ****************************************************************************/
  protected virtual void checkState()
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

      /** Move away from target. */
      case AIState.Flee:
        {
        flee();
        break;
        }
      }
    }

  /****************************************************************************
  * flee */
  /**
  * Moves tank away from target for a number of seconds defined by fleeTime.
  * 
  * @retrurns True: Fleeing. False: Not fleeing.
  ****************************************************************************/
  protected virtual bool flee()
    {
    if(target != null)
      {
      mFleeTimer += Time.deltaTime;

      /** Flee for a period of time. */
      if(mFleeTimer <= fleeTime)
        {
        /** Get vector opposite of our target. */
        Vector3 fleeVec = Vector3.Normalize(target.position - tankMotor.tf.position) * -1;

        /** Set position to flee to. */
        Vector3 fleePosition = fleeVec + tankMotor.tf.position;
        tankMotor.rotateTowards(fleePosition, tankData.turnSpeed);
        tankMotor.move(tankData.moveSpeed);
        return true;
        }

      /** Once finished fleeing, start patroling, and reset timer. */
      else
        {
        mFleeTimer = 0f;
        return false;
        }      
      }

    return false;
    }

  /****************************************************************************
  * patrol */
  /**
  * Sets a waypoint for the tank to move to, and makes it move there.
  ****************************************************************************/
  protected virtual void patrol()
    {
    Transform currentWaypoint = waypoints[mCurrentWaypointIndex];

    /** Square the difference of magnitudes, and check if it is within our
      * threshold squared. Doing this instead of using Vector3.Distance for
      * better peformance. */
    float d  = Vector3.SqrMagnitude(tankMotor.tf.position - currentWaypoint.position);
    float dt = Mathf.Pow(distanceThreshold, 2f);

    /** Move forward if finisehd rotating toward the waypoint. */
    if(tankMotor.rotateTowards(currentWaypoint.position, tankData.turnSpeed))
      {
      /** Don't move if we are at the last waypoint, and we are set to Stop
        * LooptType. */
      if(!(mCurrentWaypointIndex >= waypoints.Length - 1 && d <= dt && loopType == LoopType.Stop))
        tankMotor.move(tankData.moveSpeed);
      }

    /** Increment index to go to next waypoint. */
    if(d <= dt)
      {
      switch(loopType)
        {
        /** Loops through the waypoint list. */
        case LoopType.Loop:
          {
          mCurrentWaypointIndex = ++mCurrentWaypointIndex >= waypoints.Length ? 0 : mCurrentWaypointIndex;
          break;
          }

        /** Itrates forward if starting at 0, or backward when it gets to at
          * last index. */
        case LoopType.PingPong:
          {
          /** Iterate forward if we are at the start of the list. */
          if(mCurrentWaypointIndex <= 0)
            mPatrolForward = true;
            
          /** Iterate backward if we are at the end of the list. */
          else if(mCurrentWaypointIndex >= waypoints.Length - 1)
            mPatrolForward = false;

          /** Increment/Decrement the index. */
          mCurrentWaypointIndex += mPatrolForward ? 1 : -1;
          break;
          }

        /** Stops at the last index of the waypoint list. */
        case LoopType.Stop:
          {
          if(mCurrentWaypointIndex < waypoints.Length - 1)
            mCurrentWaypointIndex++;
          break;
          }
        }
      }
    }

  /****************************************************************************
  * rest */
  /**
  * Intentionally blank.
  ****************************************************************************/
  protected virtual void rest() { }
  }