using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TimedDestroy */
/**
* Waits a certain amount of time, then destroys the object.
******************************************************************************/
public class TimedDestroy : MonoBehaviour
  {
  public float delay;
	
  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  void Start ()
    {
    Destroy(gameObject, delay);
    }
  }
