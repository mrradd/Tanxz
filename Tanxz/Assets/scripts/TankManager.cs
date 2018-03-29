using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankManager */
/**
* Manages the Tank.
******************************************************************************/
public class TankManager : MonoBehaviour
  {
  protected bool isPlayer;

  public TankData tankData;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
  void Start()
    {
    isPlayer = gameObject.tag == "PlayerTank";
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  void Update ()
    {
    /** Health drops below 0. */
    if(tankData.hp <= 0)
      {
      if(tankData.remainingLives > 0)
        {
        ///** Spawn Player. */
        //if(isPlayer)
        //  {
        //  gameObject.transform.position = GameManager.instance.playerSpawnPoints
        //    [Random.Range(0, GameManager.instance.playerSpawnPoints.Count)].transform.position;
        //  }

        ///** Spawn AI. */
        //else
          //{
          //gameObject.transform.position = GameManager.instance.aiSpawnPoints
          //  [Random.Range(0, GameManager.instance.aiSpawnPoints.Count)].transform.position;          
          //}

        tankData.hp = tankData.baseHP;
        tankData.remainingLives--;
        tankData.isAlive = true;
        }

      /** Tank is officially dead. */
      else
        {
        /** Get materials for all child components so we can change their color. */
        Renderer[] arr = gameObject.GetComponentsInChildren<Renderer>();

        /** Change all the components color. */
        for(int i = 0; i < arr.Length; i++)
          {
          if(arr[i] == null)
            continue;

          arr[i].material.color = Color.white;
          }

        tankData.isAlive = false;
        }
      }
	  }

  }
