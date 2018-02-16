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
  public static GameManager instance;

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
