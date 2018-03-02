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

  /** Source from which the shell was fired Should be set when fired. */
  [System.NonSerialized]
  public string source = "";

  void Start()
    {
    Debug.Log(damage + " " + source);
    }

  void OnCollisionEnter(Collision collision)
    {
    //TankCollisionSection cs = collision.gameObject.GetComponent<TankCollisionSection>();

    ///** Destroy the Shell if it hit another tank. */
    //if(cs != null)
      //Destroy(gameObject);

    /** Destroy on impact. */
    Destroy(gameObject);
    }
}
