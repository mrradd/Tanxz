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

  /** AI Tank Prefab list.*/
  public List<GameObject> aiPrefabs = new List<GameObject>();

  /** AI Spawn Point. */
  [System.NonSerialized]
  public List<GameObject> aiSpawnPoints = new List<GameObject>();

  /** Pickups. */
  public List<GameObject> pickups = new List<GameObject>();

  /** Player Spawn Point. */
  [System.NonSerialized]
  public List<GameObject> playerSpawnPoints = new List<GameObject>();

  /** Players tanks. */
  public List<GameObject> playerTanks = new List<GameObject>();

  /** Player prefabs. */
  public List<GameObject> playerPrefabs = new List<GameObject>();

  /** Waypoints. */
  [System.NonSerialized]
  public List<GameObject> waypoints = new List<GameObject>();

  /** Map */
  /** Chunk height. */
  public int chunkHeight;

  /** Chunk prefabs. */
  public GameObject[] chunks;

  /** Chunk width. */
  public int chunkWidth;

  /** Number of Chunks down. */
  public int cols;

  /** Game grid of Chunks. */
  public Chunk[,] grid;

  /** Map of the day flag. */
  public bool mapOfTheDay;

  /** Map seed. */
  public int mapSeed;

  /** Number of Chunks accross. */
  public int rows;

  /** Two player game. */
  public bool twoPlayer;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Awake */
  /**
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

  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
  void Start()
    {
    if(mapOfTheDay)
      {
      System.DateTime d = new System.DateTime();
      Random.seed = d.Month + d.Day + d.Year;
      }
    else if (mapSeed > 0)
      Random.seed = mapSeed;  

    generateGrid();

    /** Spawn AI as long as we have not "used" all spawnpoints. */
    int count = 0;
    foreach(GameObject tank in aiPrefabs)
      {
      if(count >= aiSpawnPoints.Count)
        break;
      
      aiTanks.Add(Instantiate(tank, aiSpawnPoints[Random.Range(0, aiSpawnPoints.Count)].transform.position, Quaternion.identity));

      /** Assign waypoints. */
      //TODO CH  FOR SOME REASON WHEN A WAYPOINT IS ADDED TO THE LIST, THE
      //OBJECT HAS NO PROPERTIES AND DOES NOT REGISTER AS A GAMEOBJECT. 
      //List<GameObject> wp = new List<GameObject>();
      //foreach(GameObject w in waypoints)
        //{
        //tank.GetComponent<AIPersonality>().waypoints.Add(w);
        //}

      count++;
      }

    /** Instantiate the Player. */
    playerTanks.Add(Instantiate(playerPrefabs[0], playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)].transform.position, Quaternion.identity));
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * generateGrid */
  /**
  * Generates the grid.
  ****************************************************************************/
  protected void generateGrid()
    {
    grid = new Chunk[rows, cols];

    /** For each row... */
    for(int r = 0; r < rows; r++)
      {
      /** For each column... */
      for(int c = 0; c < cols; c++)
        {
        float xpos = chunkWidth * c;
        float zpos = chunkHeight * r;
        Vector3 pos = new Vector3(xpos, 0f, zpos);

        /** Create chunk object at the position. */
        GameObject tempObj = Instantiate(randomChunk(), pos, Quaternion.identity) as GameObject;

        /** Set the object's parent. */
        tempObj.transform.parent = this.transform;

        /** Give object a name. */
        tempObj.name = "Chunk-(" + r + "," + c + ")";

        /** Create temporary chunk object to manage and store. */
        Chunk chunk = tempObj.GetComponent<Chunk>();

        /** First row, drop the North Wall. */
        if(r == 0)
          {
          chunk.wallNorth.SetActive(false);
          }

        /** Last row, drop the South Wall. */
        else if(r == rows - 1)
          {
          chunk.wallSouth.SetActive(false);
          }

        /** In the middle, drop North and South Wall. */
        else
          {
          chunk.wallNorth.SetActive(false);
          chunk.wallSouth.SetActive(false);
          }

        /** In the first column, drop East Wall. */
        if(c == 0)
          {
          chunk.wallEast.SetActive(false);
          }

        /** Last column, drop West Wall. */
        else if(c == cols - 1)
          {
          chunk.wallWest.SetActive(false);
          }

        /** In the middle, drop East and West walls. */
        else
          {
          chunk.wallEast.SetActive(false);
          chunk.wallWest.SetActive(false);
          }

        /** Add the Chunk's Waypoints into the Waypoints list. */
        for(int i = 0; i < chunk.waypoints.Length; i++)
          waypoints.Add(chunk.waypoints[i]);

        /** Add the Chunk's pickup spawns to the Pickups list. */
        pickups.Add(chunk.pickupSpawn);

        /** Add the Chunk's AI spawns to the AI Spawn list. */
        aiSpawnPoints.Add(chunk.aiSpawn);

        /** Add the Chunk's Player spawns to the Player Spawn list. */
        playerSpawnPoints.Add(chunk.playerSpawn);

        /** Save the Chunk. */
        grid[r, c] = chunk;
        }
      }
    }

  /****************************************************************************
  * randomChunk */
  /**
  * Chooses a random chunk from the list.
  ****************************************************************************/
  public GameObject randomChunk()
    {
    return chunks[Random.Range(0, chunks.Length)];
    }
  }
