using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Shell */
/**
* Represents a shell launched by a cannon.
******************************************************************************/
public class Shell : MonoBehaviour
  {
  /** Damage done by the shell. Should be set when fired. */
  [System.NonSerialized]
  public int damage = 0;

  /** Sound played when it hits non-tank. */
  public AudioClip soundImpact;

  /** Source from which the shell was fired Should be set when fired. */
  [System.NonSerialized]
  public string source = "";

  /**************************************************************************
  * Start */
  /**
  **************************************************************************/
  void Start()
    {
    Debug.Log(damage + " " + source);
    }

  /**************************************************************************
  * OnCollisionEnter */
  /**
  **************************************************************************/
  void OnCollisionEnter(Collision collision)
    {
    /** Play sound if it didn't hit a tank. */
    if(collision.gameObject.tag != "AITank" && collision.gameObject.tag != "PlayerTank")
      AudioSource.PlayClipAtPoint(soundImpact, gameObject.transform.position);

    /** Destroy on impact. */
    Destroy(gameObject);
    }
}
