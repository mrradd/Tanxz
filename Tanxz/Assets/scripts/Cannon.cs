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
  /** Tank Data. */ protected TankData mTankData;
  
  /** Cannonball to launch. */ public GameObject shell;
  
  void Start()
    {
    mTankData = gameObject.GetComponentInParent<TankData>();
    }

  public void fire(float force)
    {
    GameObject s = Instantiate(shell, transform.position, Quaternion.identity);
    s.GetComponent<Shell>().damage = mTankData.shellDamage;
    s.GetComponent<Rigidbody>().AddForce(force * transform.forward);
    }
  }
