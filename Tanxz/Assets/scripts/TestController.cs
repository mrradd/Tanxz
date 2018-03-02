using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TestController */
/**
* Tests the tank controller.
******************************************************************************/
public class TestController : MonoBehaviour
  {

  public TankMotor motor;
  public TankData  tankData;

	void Start ()
    {
		
	  }
	
	void Update ()
    {
		motor.move(tankData.moveSpeed);
    motor.rotate(tankData.turnSpeed);
	  }
  }
