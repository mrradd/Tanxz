using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* AIListener */
/**
* Checks for targets making noise, and returns their transform.
******************************************************************************/
public class AIListener : MonoBehaviour
  {
  protected Transform mTarget = null;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * OnTriggerEnter */
  /**
  ****************************************************************************/
  private void OnTriggerStay(Collider other)
    {
    if(other.gameObject.CompareTag("PlayerTank"))
      gameObject.SendMessageUpwards("heardTarget", other.gameObject.transform);
    }
  }
