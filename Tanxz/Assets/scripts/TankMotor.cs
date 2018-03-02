using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankMotor */
/**
* Manages tank movement and rotation.
******************************************************************************/
[RequireComponent(typeof(Transform), typeof(TankData), typeof(CharacterController))]
public class TankMotor : MonoBehaviour
  {
  /** Our tank's Character Controller. */
  public CharacterController characterController;

  /** Tank Data. */
  public TankData tankData;

  /** GameObject's transform. */
  public Transform tf;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
	void Start ()
    {
    /** Instantiate CharacterController. */
    if(characterController == null)
      characterController = gameObject.GetComponent<CharacterController>();

    /** Instantiate TankData. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();

    /** Instantiate Transform. */
    if(tf == null)
      tf = gameObject.GetComponent<Transform>();
	  }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
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
    characterController.SimpleMove(tf.forward * speed);
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

