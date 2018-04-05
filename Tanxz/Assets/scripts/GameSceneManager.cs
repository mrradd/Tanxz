using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* GameSceneManager */
/**
* Manages the Main Game Scene.
******************************************************************************/
public class GameSceneManager : MonoBehaviour
  {
  [Header ("Prefabs")]
  /** AI Tank Prefab list.*/
  public List<GameObject> aiPrefabs = new List<GameObject>();

  /** Player prefabs. */
  public List<GameObject> playerPrefabs = new List<GameObject>();

  [Header("Grid")]
  /** Chunk prefabs. */
  public GameObject[] chunks;

  /** Chunk height. */
  public int chunkHeight;

  /** Chunk width. */
  public int chunkWidth;

  /** Number of Chunks down. */
  public int cols;

  /** Number of Chunks accross. */
  public int rows;

  /** Game grid of Chunks. */
  public Chunk[,] grid;

  [Header("Menu")]
  /** Game over menu. */
  public GameObject gameOverMenu;

  [Header("Audio")]
  /** In game music. */
  public AudioSource inGameMusicSource;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
	void Start ()
    {
    loadLevel();
	  }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  private void Update()
    {
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
        string cName = "Chunk-(" + r + "," + c + ")";
        float xpos = chunkWidth * c;
        float zpos = chunkHeight * r;
        Vector3 pos = new Vector3(xpos, 0f, zpos);

        /** Create chunk object at the position. */
        GameObject tempObj = Instantiate(randomChunk(), pos, Quaternion.identity) as GameObject;

        /** Set the object's parent. */
        tempObj.transform.parent = this.transform;

        /** Give object a name. */
        tempObj.name = cName;

        /** Create temporary chunk object to manage and store. */
        Chunk chunk = tempObj.GetComponent<Chunk>();

        /** First row, drop the North Wall. */
        if(r == 0)
          chunk.wallNorth.SetActive(false);

        /** Last row, drop the South Wall. */
        else if(r == rows - 1)
          chunk.wallSouth.SetActive(false);

        /** In the middle, drop North and South Wall. */
        else
          {
          chunk.wallNorth.SetActive(false);
          chunk.wallSouth.SetActive(false);
          }

        /** In the first column, drop East Wall. */
        if(c == 0)
          chunk.wallEast.SetActive(false);

        /** Last column, drop West Wall. */
        else if(c == cols - 1)
          chunk.wallWest.SetActive(false);

        /** In the middle, drop East and West walls. */
        else
          {
          chunk.wallEast.SetActive(false);
          chunk.wallWest.SetActive(false);
          }

        /** Add the Chunk's Waypoints into the Waypoints list. */
        for(int i = 0; i < chunk.waypoints.Length; i++)
          {
          chunk.waypoints[i].name = cName + " WP-" + i;
          GameManager.instance.waypoints.Add(chunk.waypoints[i]);
          }

        /** Name chunk spawns. */
        chunk.pickupSpawn.name = cName + " PuS";
        chunk.aiSpawn.name     = cName + " AS";
        chunk.playerSpawn.name = cName + " PS";

        /** Add the Chunk's pickup spawns to the Pickups list. */
        GameManager.instance.pickups.Add(chunk.pickupSpawn);

        /** Add the Chunk's AI spawns to the AI Spawn list. */
        GameManager.instance.aiSpawnPoints.Add(chunk.aiSpawn);

        /** Add the Chunk's Player spawns to the Player Spawn list. */
        GameManager.instance.playerSpawnPoints.Add(chunk.playerSpawn);

        /** Save the Chunk. */
        grid[r, c] = chunk;
        }
      }
    }

  /****************************************************************************
  * loadLevel */
  /**
  * Loads a game level.
  ****************************************************************************/
  public void loadLevel()
    {
    inGameMusicSource.Stop();

    /** Reset the lists. */
    GameManager.instance.aiTanks           = new List<GameObject>();
    GameManager.instance.playerTanks       = new List<GameObject>();
    GameManager.instance.playerSpawnPoints = new List<GameObject>();
    GameManager.instance.aiSpawnPoints     = new List<GameObject>();
    GameManager.instance.pickups           = new List<GameObject>();

    /** Load map of the day. */
    if(GameManager.instance.mapOfTheDay)
      {
      System.DateTime d = new System.DateTime();
      Random.seed = d.Month + d.Day + d.Year;
      }

    /** Seed the random generator. */
    else if(GameManager.instance.mapSeed > 0)
      Random.seed = GameManager.instance.mapSeed;

    generateGrid();

    /** Spawn AI as long as we have not "used" all spawnpoints. */
    int count = 0;
    foreach(GameObject tank in aiPrefabs)
      {
      if(count >= GameManager.instance.aiSpawnPoints.Count)
        break;

      /** Instantiate the tank out of the way, so its spawn code can move it to its spawn point. */
      GameManager.instance.aiTanks.Add(Instantiate(tank, new Vector3(100 * count, 10 * count), Quaternion.identity));
      count++;
      }

    /** Instantiate the Player(s). */
    if(!GameManager.instance.multiPlayer)
      GameManager.instance.playerTanks.Add(Instantiate(playerPrefabs[0], new Vector3(50, 0), Quaternion.identity));
    else
      foreach(GameObject pTank in playerPrefabs)
        GameManager.instance.playerTanks.Add(Instantiate(pTank, new Vector3(325 * count, 0), Quaternion.identity));

    /** Run audio. */
    inGameMusicSource.Play();
    inGameMusicSource.loop = true;
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
