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
  /** Index of current waypoints. */
  protected int currentWaypointIndex = 0;

  /** List of Waypoints. */
  public Transform[] waypoints;

  /** Threshhold for being close enough to waypoint. */
  public float distanceThreshold = 1f;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
	protected override void Start()
    {
    base.Start();
	  }
	
	void Update()
    {
    Transform currentWaypoint = waypoints[currentWaypointIndex];

    /** Move forward if finisehd rotating toward the waypoint. */
    if(tankMotor.rotateTowards(currentWaypoint.position, tankData.turnSpeed))
      {
      tankMotor.move(tankData.moveSpeed);
      }

    /** Square the difference of magnitudes, and check if it is within our
     * threshold squared. Doing this instead of using Vector3.Distance for speed. */
    float d  = Vector3.SqrMagnitude(tankMotor.tf.position - currentWaypoint.position);
    float dt = Mathf.Pow(distanceThreshold, 2f);

    if(d <= dt)
      {
      /** Increment index. Prevent index out of bounds. */
      currentWaypointIndex = ++currentWaypointIndex >= waypoints.Length ? 0: currentWaypointIndex;
      Debug.Log("index " + currentWaypointIndex);
      }
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  }
