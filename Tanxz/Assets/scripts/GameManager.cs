using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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

  /** Game paused flag. */
  [System.NonSerialized]
  public bool isPaused = false;

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

  /** Multiplayer game. */
  public bool multiPlayer;

  /** SFX audio mixer. */
  public AudioMixer sfxAudioMixer;

  /** SFX audio mixer. */
  public AudioMixer musicAudioMixer;

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
    //loadLevel();
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
        string  cName = "Chunk-(" + r + "," + c + ")";
        float   xpos  = chunkWidth * c;
        float   zpos  = chunkHeight * r;
        Vector3 pos   = new Vector3(xpos, 0f, zpos);

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
          {
          chunk.waypoints[i].name = cName + " WP-" + i;
          instance.waypoints.Add(chunk.waypoints[i]);
          }
          
        /** Name chunk spawns. */
        chunk.pickupSpawn.name = cName + " PuS";
        chunk.aiSpawn.name     = cName + " AS";
        chunk.playerSpawn.name = cName + " PS";

        /** Add the Chunk's pickup spawns to the Pickups list. */
        instance.pickups.Add(chunk.pickupSpawn);

        /** Add the Chunk's AI spawns to the AI Spawn list. */
        instance.aiSpawnPoints.Add(chunk.aiSpawn);

        /** Add the Chunk's Player spawns to the Player Spawn list. */
        instance.playerSpawnPoints.Add(chunk.playerSpawn);

        /** Save the Chunk. */
        instance.grid[r, c] = chunk;
        }
      }
    }

  /****************************************************************************
  * loadGameOverScene */
  /**
  * Loads the Game Over Scene.
  ****************************************************************************/
  public void loadGameOverScene()
    {
    SceneManager.LoadScene("GameOverScene");
    }

  /****************************************************************************
  * loadLevel */
  /**
  * Loads a game level.
  ****************************************************************************/
  public void loadLevel()
    {
    /** Load map of the day. */
    if(instance.mapOfTheDay)
      {
      System.DateTime d = new System.DateTime();
      Random.seed = d.Month + d.Day + d.Year;
      }

    /** Seed the random generator. */
    else if(instance.mapSeed > 0)
      Random.seed = instance.mapSeed;

    generateGrid();

    /** Spawn AI as long as we have not "used" all spawnpoints. */
    int count = 0;
    foreach(GameObject tank in instance.aiPrefabs)
      {
      if(count >= instance.aiSpawnPoints.Count)
        break;

      instance.aiTanks.Add(Instantiate(tank, instance.aiSpawnPoints[Random.Range(0, instance.aiSpawnPoints.Count)].transform.position, Quaternion.identity));

      count++;
      }

    /** Instantiate the Player(s). */
    if(!instance.multiPlayer)
      instance.playerTanks.Add(Instantiate(instance.playerPrefabs[0], instance.playerSpawnPoints[Random.Range(0, instance.playerSpawnPoints.Count)].transform.position, Quaternion.identity));
    else
      for(int i = 0; i < instance.playerPrefabs.Count; i++)
        {
        instance.playerTanks.Add(Instantiate(instance.playerPrefabs[i], instance.playerSpawnPoints[Random.Range(0, instance.playerSpawnPoints.Count)].transform.position, Quaternion.identity));
        }
    }

  /****************************************************************************
  * loadMainMenuScene */
  /**
  * Loads the Main Menu Scene.
  ****************************************************************************/
  public void loadMainMenuScene()
    {
    SceneManager.LoadScene("MainMenuScene");
    }

  /****************************************************************************
  * loadMainGameScene */
  /**
  * Loads the Maine Game Scene.
  ****************************************************************************/
  public void loadMainGameScene()
    {
    SceneManager.LoadScene("MainGameScene");
    }

  /****************************************************************************
  * randomChunk */
  /**
  * Chooses a random chunk from the list.
  ****************************************************************************/
  public GameObject randomChunk()
    {
    return instance.chunks[Random.Range(0, instance.chunks.Length)];
    }

  /****************************************************************************
  * setMusicVolume */
  /**
  * Sets the music volume.
  * @param  volume  Value to set volume to.
  ****************************************************************************/
  public void setMusicVolume(float volume)
    {
    instance.sfxAudioMixer.SetFloat("musicVolume", volume);
    }

  /****************************************************************************
  * setSFXVolume */
  /**
  * Sets the sound effects volume.
  * @param  volume  Value to set volume to.
  ****************************************************************************/
  public  void setSFXVolume(float volume)
    {
    instance.sfxAudioMixer.SetFloat("sfxVolume", volume);
    }
  }
