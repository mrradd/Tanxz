using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AIPersonality */
/**
* Base personality for controlling AI tanks and AI behavior. Base only patrols.
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

  /** Layer mask. */
  protected int mLayerMask;

  /** Follow the waypoints forward. */
  protected bool mPatrolForward = true;

  /** Temporary placeholder for Pickup target. */
  protected Transform mTargetPickup;

  /** Temporary placeholder for Tank target. */
  protected Transform mTargetTank;

  /** Time state enterred. */
  protected float mTimeStateEnterred;

  /** AI Sight component. */
  public AISight aiSight;

  /** Current AI mode. */
  public AIState aiState = AIState.Patrol;

  /** Time period move forward to avoid object. */
  public float avoidanceTime = 2.0f;

  /** Material color. */
  public Color personalityColor;

  /** Threshhold for being close enough to waypoint. */
  public float distanceThreshold = 1f;

  /** Amount of time spent fleeing. */
  public float fleeTime = 1f;

  /** Loop type. */
  public LoopType loopType;

  /** Target to chase. */
  public Transform target;

  /** List of Waypoints. */
  public List<GameObject> waypoints = new List<GameObject>();

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Awake */
  /**
  ****************************************************************************/
  protected virtual void Awake()
    {
    /** Make sure the tank can see. */
    if(aiSight == null)
      aiSight = gameObject.GetComponent<AISight>();

    /** Set Layer Mask. */
    mLayerMask = 1 << LayerMask.NameToLayer("Interractive");
    }

  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  protected override void Start()
    {
    base.Start();

    changeColor(personalityColor);

    initWaypoints();
    }

  /**************************************************************************
  * Update */
  /**
  **************************************************************************/
  protected virtual void Update()
    {
    /** If dead, do nothing. */
    if(!tankData.isAlive || GameManager.instance.isPaused || GameManager.instance.gameOver) 
      return;
    
    scanForTarget();

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
  * avoid */
  /**
  * Attempts to avoid an obstacle if one is encountered.
  ****************************************************************************/
  protected virtual void avoid()
    {
    /** Rotate the tank, and advance avoidance stage. */
    if(mAvoidanceStage == 1)
      {
      /** Turn left. */
      tankMotor.rotate(-1 * tankData.turnSpeed);

      /** Change avoidance stage, and reset timer. */
      //if(canMove() && isPerpendicular())
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
    if(Physics.Raycast(tankMotor.tf.position, tankMotor.tf.forward, out hit, tankData.moveSpeed, mLayerMask))
      {
      return false;
      }

    return true;
    }

  /****************************************************************************
  * changeColor */
  /**
  * Changes the tanks color.
  * 
  * @param  color  Color to change tank to.
  ****************************************************************************/
  protected virtual void changeColor(Color color)
    {
    /** Get materials for all child components so we can change their color. */
    Renderer[] arr = gameObject.GetComponentsInChildren<Renderer>();

    /** Change all the components color. */
    for(int i = 0; i < arr.Length; i++)
      {
      if(arr[i] == null)
        continue;

      arr[i].material.color = color;
      }
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
      float d  = Vector3.SqrMagnitude(tankMotor.tf.position - target.position);
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
      }
    }

  /****************************************************************************
  * fire */
  /**
  * Fires forward.
  ****************************************************************************/
  protected virtual void fire()
    {
    firingMechanism.fire(tankData.cannonForce);
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
    if(hasActiveTarget())
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

      /** Once finished fleeing, remove target, start patroling, and reset timer. */
      else
        {
        target     = null;
        aiState    = AIState.Patrol;
        mFleeTimer = 0f;

        return false;
        }      
      }

    return false;
    }

  /****************************************************************************
  * hasActiveTarget */
  /**
  * Verifies if has target that is alive.
  * 
  * @returns  True: alive target. False: no alive target.
  ****************************************************************************/
  protected virtual bool hasActiveTarget()
    {
    return target != null && target.gameObject.GetComponent<BaseData>().isAlive;
    }

  /****************************************************************************
  * heardTarget */
  /**
  * AI Listener calls this when a target is found.
  * 
  * @param  tf  Transform of new target.
  ****************************************************************************/
  public virtual void heardTarget(Transform tf)
    {
    Debug.Log("Heard " + tf.gameObject.name);
    }

  /****************************************************************************
  * initWaypoints */
  /**
  * Initializes the waypoint list.
  ****************************************************************************/
  protected virtual void initWaypoints()
    {
    /** Global waypoint list. */       List<GameObject> wp    = GameManager.instance.waypoints;
    /** List of assigned waypoints. */ List<int>        iList = new List<int>();

    /** Iterate over the global waypoint list and randomly add a waypoint to the list. */
    for(int i = 0; i < wp.Count; i++)
      {
      int j = Random.Range(0, wp.Count);
      if(!iList.Contains(j))
        {
        iList.Add(j);
        waypoints.Add(wp[j]);
        }
      }
    }

  ///****************************************************************************
  //* isPerpendicular */
  ///**
  //* Checks if tank is perpendicular to obstacle.
  //* 
  //* @returns  True: is perpendicular. False: not perpendicular.
  //****************************************************************************/
  //protected virtual bool isPerpendicular()
    //{
    ////TODO CH  CAN'T GET THIS TO WORK.....
    //RaycastHit hitInfo;

    //if(Physics.Raycast(tankMotor.tf.position, tankMotor.tf.right, out hitInfo, 10, mLayerMask))
    //  {
    //  Vector3 p1 = Vector3.Normalize(hitInfo.point);
    //  Vector3 p2 = Vector3.Normalize(tankMotor.tf.right);
    //  float dp = Vector3.Dot(p1, p2);

    //  Debug.DrawRay(tankMotor.tf.position, tankMotor.tf.right, Color.red, 1f);

    //  if(dp <= .1 || dp >= -.1)
    //    {
    //    return true; 
    //    }
    //  else
    //    {
    //    return false;
    //    }
    //  }

    //return false;
    //}

  /****************************************************************************
  * patrol */
  /**
  * Sets a waypoint for the tank to move to, and makes it move there.
  ****************************************************************************/
  protected virtual void patrol()
    {
    if(waypoints.Count <= 0)
      return;
    
    if(waypoints[mCurrentWaypointIndex] != null)
      {
      Transform currentWaypoint = waypoints[mCurrentWaypointIndex].transform;

      /** Square the difference of magnitudes, and check if it is within our
        * threshold squared. Doing this instead of using Vector3.Distance for
        * better peformance. */
      float d = Vector3.SqrMagnitude(tankMotor.tf.position - currentWaypoint.position);
      float dt = Mathf.Pow(distanceThreshold, 2f);

      /** Move forward if finisehd rotating toward the waypoint. */
      if(tankMotor.rotateTowards(currentWaypoint.position, tankData.turnSpeed))
        {
        /** Don't move if we are at the last waypoint, and we are set to Stop
          * LooptType. */
        if(!(mCurrentWaypointIndex >= waypoints.Count - 1 && d <= dt && loopType == LoopType.Stop))
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
            mCurrentWaypointIndex = ++mCurrentWaypointIndex >= waypoints.Count ? 0 : mCurrentWaypointIndex;
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
            else if(mCurrentWaypointIndex >= waypoints.Count - 1)
              mPatrolForward = false;

            /** Increment/Decrement the index. */
            mCurrentWaypointIndex += mPatrolForward ? 1 : -1;
            break;
            }

          /** Stops at the last index of the waypoint list. */
          case LoopType.Stop:
            {
            if(mCurrentWaypointIndex < waypoints.Count - 1)
              mCurrentWaypointIndex++;
            break;
            }
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

  /****************************************************************************
  * scanForTarget */
  /**
  * Scans the area for a target. Default only searches for Player.
  ****************************************************************************/
  protected virtual void scanForTarget()
    {
    target = aiSight.scanForType(AISight.TargetType.PlayerTank);
    }
  }
