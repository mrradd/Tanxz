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
  [System.NonSerialized]
  /** Damage done by the shell. Should be set when fired. */
  public int damage = 0;

  [System.NonSerialized]
  /** Source from which the shell was fired. */
  public string source = "";

  void Start()
    {
    Debug.Log(damage + " " + source);
    }

  void OnCollisionEnter(Collision collision)
    {
    TankCollisionSection cs = collision.gameObject.GetComponent<TankCollisionSection>();

    /** Destroy the Shell if it hit another tank. */
    if(cs != null)
      Destroy(gameObject);
    }
}
