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
  protected DoomHeadAnimManager mDHAM;

  /** Is a player flag. */
  protected bool mIsPlayer;

  /** Tank Data. */
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
    mIsPlayer = gameObject.tag == "PlayerTank";

    if(mDHAM == null && mIsPlayer)
      {
      mDHAM = GetComponentInChildren<DoomHeadAnimManager>();
      }
      
    spawn();
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
        spawn();

        tankData.hp = tankData.baseHP;
        tankData.remainingLives--;
        tankData.isAlive = true;
        }

      /** Tank is officially dead. */
      else
        {
        GameManager.instance.checkGameOver();

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

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * spawn */
  /**
  * Spawns the tank at one of the positions in the spawn point list.
  ****************************************************************************/
  protected void spawn()
    {
    if(mDHAM != null)
      mDHAM.changeToIdleState();
    
    List<GameObject> spawnList = mIsPlayer ? GameManager.instance.playerSpawnPoints : GameManager.instance.aiSpawnPoints;
    gameObject.transform.position = spawnList[Random.Range(0, spawnList.Count)].transform.position;
    }
  }
