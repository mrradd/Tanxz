using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* GameManager */
/**
* Singleton that manages all aspects of the Game.
******************************************************************************/
public class GameManager : MonoBehaviour
  {
  /** Instance accessor. */
  public static GameManager instance;

  /** AI tanks. */
  public List<GameObject> aiTanks = new List<GameObject>();

  /** Players tanks. */
  public List<GameObject> playerTanks = new List<GameObject>();

  /** Powerups */
  public List<GameObject> powerups = new List<GameObject>();

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  void Awake()
    {
    if(instance == null)
      instance = this;
    else
      {
      Debug.LogError("There can only be one instance of GameManager.");
      Destroy(gameObject);
      }
    }
  }
