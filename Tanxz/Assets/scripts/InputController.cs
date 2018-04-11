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
  protected DoomHeadAnimManager mDHAM;

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

    if(mDHAM == null)
      mDHAM = GetComponentInChildren<DoomHeadAnimManager>();
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
      {
      stopTankMovingSound();
      return;
      }
      
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

    /** Apply damage. */
    if(Input.GetKeyDown(KeyCode.Alpha6))
      SendMessage("applyDamage", new ApplyDamagePayload(34, "Test"));

    /** Toggle God Mode. */
    if(Input.GetKeyDown(KeyCode.G))
      tankData.godMode = !tankData.godMode;

    /** Exit game. */
    if(Input.GetKeyDown(KeyCode.Escape))
      Application.Quit();
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
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);
        bool a = Input.GetKey(KeyCode.A);

        /** Forward. */
        if(w)
          tankMotor.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(s)
          tankMotor.move(-tankData.moveSpeed);

        /** Right. */
        if(d)
          tankMotor.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(a)
          tankMotor.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.Space))
          {
          firingMechanism.fire(tankData.cannonForce);
          mDHAM.changeToShootingState();
          }

        /** Play tank moving soud. */
        if(w || s || a || d)
          playTankMovingSound();
          
        /** Stop tank moving sound. */
        else
          stopTankMovingSound();
            
        break;
        }

      /** Arrowkey input. */
      case InputScheme.arrowKeys:
        {
        bool u = Input.GetKey(KeyCode.UpArrow);
        bool d = Input.GetKey(KeyCode.DownArrow);
        bool r = Input.GetKey(KeyCode.RightArrow);
        bool l = Input.GetKey(KeyCode.LeftArrow);

        /** Forward. */
        if(u)
          tankMotor.move(tankData.moveSpeed);
        
        /** Reverse. */
        else if(d)
          tankMotor.move(-tankData.moveSpeed);

        /** Right. */
        if(r)
          tankMotor.rotate(tankData.turnSpeed);
        
        /** Left. */
        else if(l)
          tankMotor.rotate(-tankData.turnSpeed);

        /** Fire. */
        if(Input.GetKey(KeyCode.Period))
          {
          firingMechanism.fire(tankData.cannonForce);
          mDHAM.changeToShootingState();
          }

        /** Play tank moving soud. */
        if(u || d || r || l)
          playTankMovingSound();
          
        /** Stop tank moving sound. */
        else
          stopTankMovingSound();
          
        break;
        }
      }
    }
  }
