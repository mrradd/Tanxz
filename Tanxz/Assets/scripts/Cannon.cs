using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Cannon */
/**
* Fires a cannonball forward.
******************************************************************************/
public class Cannon : MonoBehaviour
  {
  /** Cannonball to launch. */ public GameObject cannonBall;
	
  public void fire(float force)
    {
    GameObject cb = Instantiate(cannonBall, transform.position, Quaternion.identity);
    cb.GetComponent<Rigidbody>().AddForce(force * transform.forward);
    }
  }
