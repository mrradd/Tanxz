using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Cannon */
/**
* Controls firing shells.
******************************************************************************/
public class Cannon : MonoBehaviour
  {
  /** Tank Data. */ protected TankData mTankData;
  
  /** Cannonball to launch. */ public GameObject shell;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  void Start()
    {
    mTankData = gameObject.GetComponentInParent<TankData>();
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * fire */
  /**
  * Fires a shell forward at a given force. Applies the passed in tag to the
  * shell for firing source identification.
  * 
  * @param  force      Force to fire shell at.
  * @param  sourceTag  Tag from source to apply to the shell.
  ****************************************************************************/
  public void fire(float force, string sourceTag)
    {
    GameObject s = Instantiate(shell, transform.position, Quaternion.identity);
    Shell sh = s.GetComponent<Shell>();

    sh.GetComponent<Shell>().damage = mTankData.shellDamage;
    sh.GetComponent<Shell>().source = sourceTag;

    s.GetComponent<Rigidbody>().AddForce(force * transform.forward);
    }
  }
