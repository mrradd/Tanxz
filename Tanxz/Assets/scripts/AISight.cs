using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AISight */
/**
* Uses RayCasts to search for the tanks, and pickups.
******************************************************************************/
public class AISight : MonoBehaviour
  {
  public enum TargetType { PlayerTank, AITank, Pickup };

  protected int mLayerMask;

  /** List of AI tanks. */
  protected List<GameObject> mAITanks = new List<GameObject>();

  /** List of player tanks. */
  protected List<GameObject> mPlayerTanks = new List<GameObject>();

  /** List of Powerups. */
  protected List<GameObject> mPickups = new List<GameObject>();

  /** Tank Data component. */
  public TankData tankData;

  /** Transform. */
  public Transform tf;

  /**************************************************************************
  * Unity Methods 
  **************************************************************************/
  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  void Start()
    {
    mPlayerTanks = GameManager.instance.playerTanks;
    mAITanks     = GameManager.instance.aiTanks;
    mPickups     = GameManager.instance.pickups;

    if(tf == null)
      tf = gameObject.GetComponent<Transform>();

    mLayerMask = 1 << LayerMask.NameToLayer("Interractive");
    }

  /**************************************************************************
  * Methods 
  **************************************************************************/
  /**************************************************************************
  * canSee */
  /**
  * Searches for the passed in target.
  **************************************************************************/
  protected GameObject canSee(GameObject target)
    {
    Vector3 targetPosition = target.transform.position;

    /** Find the vector from the agent to the target We do this by subtracting
     * "destination minus origin", so that "origin plus vector equals destination." */
    Vector3 agentToTargetVector = targetPosition - tf.position;

    /** Find the angle between the direction our agent is facing (forward in
     * local space) and the vector to the target. */
    float angleToTarget = Vector3.Angle(agentToTargetVector, tf.forward);

    /** Check if that angle is less than our field of view */
    if(angleToTarget < tankData.fieldOfView)
      {
      /** Ray from our position to the target */
      Ray rayToTarget = new Ray();

      /** Set the origin of the ray to our position, and the direction to the
       * vector to the target */
      rayToTarget.origin = tf.position;
      rayToTarget.direction = agentToTargetVector;

      /** Create a variable to hold information about anything the ray
       * collides with */
      RaycastHit hitInfo;

      /** Cast ray from the tank for the view distance to see if we hit the
       * target. Filtering for only objects in the Interractive layer.  */
      if(Physics.Raycast(rayToTarget, out hitInfo, tankData.viewDistance, mLayerMask))
        {
        if(hitInfo.collider.gameObject == target)
          {
          return hitInfo.collider.gameObject;
          }
        }
      }

    /* Did not hit the target. */
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
  public Transform scanForType(TargetType type)
    {
    GameObject target;

    /** Choose list of targets. */
    List<GameObject> targets = type == TargetType.AITank ? mAITanks :
                               type == TargetType.Pickup ? mPickups : mPlayerTanks;

    //TODO CH  REFACTOR SO IT TARGETS CLOSEST OBJECT.
    /** See if a target is in sight. */
    for(int i = 0; i < targets.Count; i++)
      {
      target = canSee(targets[i]);

      if(target != null && target.gameObject.GetComponent<BaseData>().isAlive)
        return target.transform;
      }

    return null;
    }
  }
