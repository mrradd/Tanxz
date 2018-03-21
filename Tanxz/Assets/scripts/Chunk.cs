using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Chunk */
/**
* A piece of the grid map. Has four sides, a Player spawn, and AI spawn, one or
* more Waypoints, and a Pickup spawn.
******************************************************************************/
public class Chunk : MonoBehaviour
  {
  public GameObject   wallNorth;
  public GameObject   wallSouth;
  public GameObject   wallEast;
  public GameObject   wallWest;
  public GameObject   playerSpawn;
  public GameObject   aiSpawn;
  public GameObject   pickupSpawn;
  public GameObject[] waypoints;
  }
