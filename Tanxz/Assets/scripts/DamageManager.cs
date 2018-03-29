using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* DamageManager */
/**
* Handles the application of damage.
******************************************************************************/
[RequireComponent(typeof(TankData))]
public class DamageManager : MonoBehaviour
  {
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
  void Start()
    {
    /** Instantiate TankData. */
    if(tankData == null)
      tankData = gameObject.GetComponent<TankData>();
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * applyDamage */
  /**
  * Method to call when a Collidable Section of the tank was hit.
  * 
  * @param  adp  ApplyDamagePayload object.
  ****************************************************************************/
  public void applyDamage(ApplyDamagePayload adp)
    {
    if(!tankData.godMode)
      tankData.hp -= adp.damage;

    Debug.Log(tankData.hp + " " + adp.damage + " " + adp.source);

    /** Check if tank is dead. */
    if(tankData.hp <= 0)
      {
      Debug.Log(tankData.name + " died.");

      /** Increase the score of the opposing that hit this tank. */
      GameObject.Find(adp.source).GetComponent<TankData>().score += tankData.pointValue;

      //tankData.audioSource.PlayOneShot(tankData.soundTankDestroyed);
      AudioSource.PlayClipAtPoint(tankData.soundTankDestroyed, gameObject.transform.position, tankData.audioSource.volume);
      }

    /** Play tank hit sound. */
    else
      {
      //tankData.audioSource.PlayOneShot(tankData.soundTankHit);
      AudioSource.PlayClipAtPoint(tankData.soundTankHit, gameObject.transform.position, tankData.audioSource.volume);
      }
    }
  }

/******************************************************************************
* Class: ApplyDamagePayload */
/**
* Container object which allows for messaging DamageManager.applyDamage with
* multiple parameters.
******************************************************************************/
public class ApplyDamagePayload
  {
  /** Amount of damage to be applied. */
  public int damage;

  /** Source the damage is coming from. */
  public string source;

  /** Constructor */
  public ApplyDamagePayload() { }
  public ApplyDamagePayload(int damage, string source)
    {
    this.damage = damage;
    this.source = source;
    }
  }