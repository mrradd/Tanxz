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
  /** Controller input types. */
  public enum InputScheme {WASD, arrowKeys};

  /** Selected Input type. */   
  public InputScheme input = InputScheme.WASD;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  protected override void Start()
    {
    base.Start();
    }

  /**************************************************************************
  * Update */
  /**
  **************************************************************************/
  private void Update()
    {
    checkSpecialCommands();

    /** Prevent controls when dead. */
    if(!tankData.isAlive || GameManager.instance.isPaused || GameManager.instance.gameOver)
      return;

    controls();
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * checkSpecialCommands */
  /**
  * Checks special commands entered.
  ****************************************************************************/
  public void checkSpecialCommands()
    {
    /** Toggle Paused. */
    if(Input.GetKeyDown(KeyCode.Alpha9))
      GameManager.instance.isPaused = !GameManager.instance.isPaused;
    
    /** Go back to Main Menu Scene. */
    if(Input.GetKeyDown(KeyCode.Alpha0))
      GameManager.instance.loadMainMenuScene();

    /** Game over. */
    if(Input.GetKeyDown(KeyCode.Alpha8))
      {
      GameManager.instance.gameOver = true;
      GameManager.instance.showGameOverMenu();
      }

    /** Increase player 1 score. */
    if(Input.GetKeyDown(KeyCode.Alpha7))
      GameManager.instance.playerTanks[0].GetComponent<TankData>().score += 10;

    /** Toggle God Mode. */
    if(Input.GetKeyDown(KeyCode.G))
      tankData.godMode = !tankData.godMode;
    }

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
          firingMechanism.fire(tankData.cannonForce);

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
        if(Input.GetKey(KeyCode.Period))
          firingMechanism.fire(tankData.cannonForce);

        break;
        }
      }
    }
  }
