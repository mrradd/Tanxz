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
  /** Instance accessor. */ public static GameManager instance;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  void Awake()
    {
    if(instance == null)
      instance = this;
    else
      {
      Debug.LogError("There can only be on instance of GameManager.");
      Destroy(gameObject);
      }
    }
  }
