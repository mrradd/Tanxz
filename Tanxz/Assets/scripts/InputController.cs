using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* InputController */
/**
* Player controls from keyboard/arrowkeys/gamepad etc... Must have TankData and
* TankMotor objects.
******************************************************************************/
[RequireComponent(typeof(TankData)), RequireComponent(typeof(TankMotor))]
public class InputController : MonoBehaviour
  {
  /** Controller input types. */ public enum InputScheme {WASD, arrowKeys};
  /** Selected Input type. */    public InputScheme input = InputScheme.WASD;
  /** Tank data. */              public TankData    tankData;
  /** Tank motor. */             public TankMotor   tankMotor;

  /**************************************************************************
  * Unity Methods 
  **************************************************************************/
  private void Start()
    {
    /** Make sure we instantiate Tank Data if we don't have one. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();

    /** Make sure we instantiate Tank Motor if we don't have one. */
    if(tankMotor == null)
      tankMotor = gameObject.GetComponent<TankMotor>();
    }

  private void Update()
    {
    controls();
    }

  /**************************************************************************
  * Methods 
  **************************************************************************/
  /**************************************************************************
  * controls */ 
  /**
  * Controls tank movement and firing based on controller scheme.
  **************************************************************************/
  public void controls()
    {
    switch(input)
      {
      /** WASD input. */
      case InputScheme.WASD:
        {
        /** Forward. */
        if(Input.GetKey(KeyCode.W))
          tankMotor.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(Input.GetKey(KeyCode.S))
          tankMotor.move(-tankData.moveSpeed);

        /** Right. */
        if(Input.GetKey(KeyCode.D))
          tankMotor.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(Input.GetKey(KeyCode.A))
          tankMotor.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.Space))
          tankMotor.fire(tankData.cannonForce);

        break;
        }

      /** Arrowkey input. */
      case InputScheme.arrowKeys:
        {
        /** Forward. */
        if(Input.GetKey(KeyCode.UpArrow))
          tankMotor.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(Input.GetKey(KeyCode.DownArrow))
          tankMotor.move(-tankData.moveSpeed);

        /** Right. */
        if(Input.GetKey(KeyCode.RightArrow))
          tankMotor.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(Input.GetKey(KeyCode.LeftArrow))
          tankMotor.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.RightControl))
          tankMotor.fire(tankData.cannonForce);

        break;
        }
      }
    }
  }
