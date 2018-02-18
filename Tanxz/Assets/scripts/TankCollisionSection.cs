using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* CollisionSection */
/**
* Section of the GameObject which can be hit. Makes a call to its parent to
* tell it that it was hit.
******************************************************************************/
public class TankCollisionSection : MonoBehaviour
  {
  private void OnCollisionEnter(Collision collision)
    {
    if(collision.gameObject.tag == "Shell")
      {
      Shell shell = collision.gameObject.GetComponent<Shell>();

      SendMessageUpwards("applyDamage", new ApplyDamagePayload(shell.damage, shell.source));
      }
    }
  }
