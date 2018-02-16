using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankMotor */
/**
* Controls the movement and firing of the tank.
******************************************************************************/
public class TankMotor : MonoBehaviour
  {
  /** Our tank's Character Controller. */ protected CharacterController mCharacterController;
  /** Tank's Cannon. */                   protected Cannon              mCannon;
  /** Time until next shot. */            protected float               mShotTimer;

  /** GameObject's transform. */ public Transform tf;

  /**************************************************************************
  * Unity Methods 
  **************************************************************************/
	void Start ()
    {
		mCharacterController = gameObject.GetComponent<CharacterController> ();
    mCannon              = gameObject.GetComponentInChildren<Cannon>();
    tf                   = gameObject.GetComponent<Transform>();
	  }

  void Update()
    {
    /** Update the shot timer. */
    mShotTimer += Time.deltaTime;
    }

  /**************************************************************************
  * Methods 
  **************************************************************************/
  /**************************************************************************
  * fire */
  /**
  * Fires a cannonball.
  * 
  * @param  force  Force to fire cannon at.
  **************************************************************************/
  public void fire(float force)
    {
    /** Fire a cannonball */
    if(mShotTimer >= gameObject.GetComponent<TankData>().firingDelay)
      {
      mShotTimer = 0f;
      mCannon.fire(force);
      }
    }

  /**************************************************************************
  * move */ 
  /**
  * Moves the tank forward and backward.
  * 
  * @param  speed  Negative is backward, positive is forward.
  **************************************************************************/
  public void move(float speed)
    {
    /** Get the gameobject's current facing (forward) then change the direction
     *  of movement by applying speed. */
    mCharacterController.SimpleMove(tf.forward * speed);
    }

  /**************************************************************************
  * rotate */ 
  /**
  * Change the facing of the tank.
  * 
  * @param  rotationSpeed  Negative is left, positive is right. 
  **************************************************************************/
  public void rotate(float rotationSpeed)
    {
    /** Rotate the gameobject about the local Y axis at the passed in speed. */
    tf.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
  }
