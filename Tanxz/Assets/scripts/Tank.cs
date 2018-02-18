using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Tank */
/**
* Manages tank control and state.
******************************************************************************/
public class Tank : MonoBehaviour
  {
  /** Our tank's Character Controller. */ protected CharacterController mCharacterController;
  /** Tank's Cannon. */                   protected Cannon              mCannon;
  /** Time until next shot. */            protected float               mShotTimer;
  /** Tank Data. */                       protected TankData            mTankData;

  /** GameObject's transform. */ public Transform tf;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
	void Start ()
    {
		mCharacterController = gameObject.GetComponent<CharacterController>();
    mCannon              = gameObject.GetComponentInChildren<Cannon>();
    mTankData            = gameObject.GetComponent<TankData>();
    tf                   = gameObject.GetComponent<Transform>();
	  }

  void Update()
    {
    /** Update the shot timer. */
    mShotTimer += Time.deltaTime;
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * fire */
  /**
  * Fires a cannonball.
  * 
  * @param  force  Force to fire cannon at.
  ****************************************************************************/
  public void fire(float force)
    {
    /** Fire a cannonball */
    if(mShotTimer >= gameObject.GetComponent<TankData>().firingDelay)
      {
      mShotTimer = 0f;
      mCannon.fire(force, gameObject.name);
      }
    }

  /****************************************************************************
  * applyDamage */
  /**
  * Method to call when a Collidable Section of the tank was hit.
  * 
  * @param  adp  ApplyDamagePayload object.
  ****************************************************************************/
  public void applyDamage(ApplyDamagePayload adp)
    {
    mTankData.hp -= adp.damage;

    Debug.Log(mTankData.hp + " " + adp.damage + " " + adp.source);

    /** Check if tank is dead. */
    if(mTankData.hp <= 0)
      {
      Debug.Log(mTankData.name + " dead.");

      /** Increase the score of the object that hit this tank. */
      GameObject.Find(adp.source).GetComponent<TankData>().score += mTankData.pointValue;

      Destroy(gameObject);
      }
    }

  /****************************************************************************
  * move */ 
  /**
  * Moves the tank forward and backward.
  * 
  * @param  speed  Negative is backward, positive is forward.
  ****************************************************************************/
  public void move(float speed)
    {
    /** Get the gameobject's current facing (forward) then change the direction
     *  of movement by applying speed. */
    mCharacterController.SimpleMove(tf.forward * speed);
    }

  /****************************************************************************
  * rotate */ 
  /**
  * Change the facing of the tank.
  * 
  * @param  rotationSpeed  Negative is left, positive is right. 
  ****************************************************************************/
  public void rotate(float rotationSpeed)
    {
    /** Rotate the gameobject about the local Y axis at the passed in speed. */
    tf.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
  }

/******************************************************************************
* Class: ApplyDamagePayload */
/**
* Container object which allows for messaging Tank.applyDamage with multiple
* parameters
******************************************************************************/
public class ApplyDamagePayload
  {
  /** Amount of damage to be applied. */   public int    damage;
  /** Source the damage is coming from. */ public string source;

  /** Constructor */
  public ApplyDamagePayload(){}
  public ApplyDamagePayload(int damage, string source)
    {
    this.damage = damage;
    this.source = source;
    }
  }
