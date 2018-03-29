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

  /** Shell to launch. */
  public GameObject shell;

  /** Owning Tank's Data. */
  [System.NonSerialized]
  public TankData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  public virtual void Start()
    {
    /** Instantiate TankData. */
    if(tankData == null)
      tankData = gameObject.GetComponentInParent<TankData>();
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
  public virtual void fire(float force, string sourceTag)
    {
    GameObject s = Instantiate(shell, transform.position, Quaternion.identity);
    Shell sh = s.GetComponent<Shell>();

    sh.GetComponent<Shell>().damage = tankData.shellDamage;
    sh.GetComponent<Shell>().source = sourceTag;

    s.GetComponent<Rigidbody>().AddForce(force * transform.forward);
    }
  }
