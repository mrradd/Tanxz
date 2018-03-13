using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AISight */
/**
* Uses RayCasts to search for the tanks, and powerups.
******************************************************************************/
public class AISight : MonoBehaviour
  {
  /** List of AI tanks. */
  protected List<GameObject> mAITanks = new List<GameObject>();

  /** Distance to player. */
  protected float mDistance = 0f;

  /** List of player tanks. */
  protected List<GameObject> mPlayerTanks = new List<GameObject>();

  /** List of Powerups. */
  protected List<GameObject> mPowerups = new List<GameObject>();

  /** Range of vision angle. */
  public float fieldOfView = 45.0f;

  /** Range of vision distance. */
  public float rangeOfVision = 15f;

  /**************************************************************************
  * Unity Methods 
  **************************************************************************/
  /**************************************************************************
  * Awake */
  /**
  **************************************************************************/
  void Awake()
    {
    mPlayerTanks = GameManager.instance.playerTanks;
    mAITanks     = GameManager.instance.aiTanks;
    mPowerups    = GameManager.instance.powerups;
    }

  /**************************************************************************
  * Update */
  /**
  **************************************************************************/
  void Update()
    {
    }

  /**************************************************************************
  * Methods 
  **************************************************************************/
  /**************************************************************************
  * canSee */
  /**
  * Searches for the passed in target.
  **************************************************************************/
  public GameObject canSee(GameObject target)
    {
    Vector3 targetPosition = target.transform.position;

    mDistance = Vector3.Distance(target.transform.position, transform.position);

    /* Find the vector from the agent to the target We do this by subtracting
     * "destination minus origin", so that "origin plus vector equals destination." */
    Vector3 agentToTargetVector = targetPosition - transform.position;

    /* Find the angle between the direction our agent is facing (forward in
     * local space) and the vector to the target. */
    float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

    /* if that angle is less than our field of view */
    if(angleToTarget < fieldOfView)
      {
      /* Create a variable to hold a ray from our position to the target */
      Ray rayToTarget = new Ray();

      /* Set the origin of the ray to our position, and the direction to the
       * vector to the target */
      rayToTarget.origin = transform.position;
      rayToTarget.direction = agentToTargetVector;

      /* Create a variable to hold information about anything the ray
       * collides with */
      RaycastHit hitInfo;

      /* Cast our ray for infinity in the direciton of our ray. -- If we
       * hit something... */
      if(Physics.Raycast(rayToTarget, out hitInfo, Mathf.Infinity))
        {
        /* ... and that something is our target */
        if(hitInfo.collider.gameObject == target && mDistance <= rangeOfVision)
          {
          return hitInfo.collider.gameObject;
          }
        }
      }

    /* We hit nothing or if we hit something that is not our target. */
    return null;
    }

  /**************************************************************************
  * scanForAITank */
  /**
  * Scans the area for targets based on passed in type and returns the first
  * one found. Defaults to searching for Players.
  * 
  * @param  type  Type of target to scan for.
  * @returns  GameObject of target. Null if nothing found.
  **************************************************************************/
  public GameObject scanForType(string type)
    {
    GameObject target;

    List<GameObject> targets = type == "AITank" ? mAITanks : type == "Powerup" ? mPowerups : mPlayerTanks;

    for(int i = 0; i < targets.Count; i++)
      {
      target = canSee(targets[i]);

      if(target != null)
        return target;
      }

    return null;
    }
  }
