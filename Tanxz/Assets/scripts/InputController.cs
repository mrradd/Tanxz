using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* InputController */
/**
* Player controls from keyboard/arrowkeys/gamepad etc... Must have TankData and
* TankMotor objects.
******************************************************************************/
public class InputController : BaseController
  {
  /** Controller input types. */ public enum InputScheme {WASD, arrowKeys};
  /** Selected Input type. */    public InputScheme input = InputScheme.WASD;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  protected override void Start()
    {
    base.Start();
    }

  private void Update()
    {
    controls();
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * controls */ 
  /**
  * Controls tank movement and firing based on controller scheme.
  ****************************************************************************/
  public void controls()
    {
    switch(input)
      {
      /** WASD input. */
      case InputScheme.WASD:
        {
        /** Forward. */
        if(Input.GetKey(KeyCode.W))
          tank.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(Input.GetKey(KeyCode.S))
          tank.move(-tankData.moveSpeed);

        /** Right. */
        if(Input.GetKey(KeyCode.D))
          tank.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(Input.GetKey(KeyCode.A))
          tank.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.Space))
          tank.fire(tankData.cannonForce);

        break;
        }

      /** Arrowkey input. */
      case InputScheme.arrowKeys:
        {
        /** Forward. */
        if(Input.GetKey(KeyCode.UpArrow))
          tank.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(Input.GetKey(KeyCode.DownArrow))
          tank.move(-tankData.moveSpeed);

        /** Right. */
        if(Input.GetKey(KeyCode.RightArrow))
          tank.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(Input.GetKey(KeyCode.LeftArrow))
          tank.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.RightControl))
          tank.fire(tankData.cannonForce);

        break;
        }
      }
    }
  }
