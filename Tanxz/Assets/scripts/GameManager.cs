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

  /** AI Spawn Point. */
  [System.NonSerialized]
  public List<GameObject> aiSpawnPoints = new List<GameObject>();

  /** Game Over flag. */
  public bool gameOver = false;

  /** High score. */
  public int highScore = 0;

  /** Game paused flag. */
  [System.NonSerialized]
  public bool isPaused = false;

  /** Map of the day flag. */
  public bool mapOfTheDay = false;

  /** Map seed. */
  public int mapSeed;

  /** Multiplayer game. */
  public bool multiPlayer = false;

  /** Pickups. */
  public List<GameObject> pickups = new List<GameObject>();

  /** Player Spawn Point. */
  [System.NonSerialized]
  public List<GameObject> playerSpawnPoints = new List<GameObject>();

  /** Players tanks. */
  public List<GameObject> playerTanks = new List<GameObject>();

  /** Waypoints. */
  [System.NonSerialized]
  public List<GameObject> waypoints = new List<GameObject>();

  /** Sound */
  /** Master Audio mixer. */
  public AudioMixer masterAudioMixer;

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
      {
      instance = this;
      loadSettings();
      }
    else
      {
      Debug.LogError("There can only be one instance of GameManager.");
      Destroy(gameObject);
      }
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * calculateHighScore */
  /**
  * Compares current highscore and Player scores to determine the highest.
  ****************************************************************************/
  public void calculateHighScore()
    {
    loadHighScore();

    foreach(GameObject tank in instance.playerTanks)
      {
      int s = tank.GetComponent<TankData>().score;
      instance.highScore = s > instance.highScore ? s : instance.highScore;
      }

    saveHighScore();
    }

  /****************************************************************************
  * checkGameOver */
  /**
  * Checks for game over state and shows Game Over Scene.
  ****************************************************************************/
  public void checkGameOver()
    {
    bool enemiesDefeated = false;
    bool playersDefeated = false;

    /** Check AI tanks destroyed. */
    int aicount = 0;
    foreach(GameObject aiTank in instance.aiTanks)
      {
      if(!aiTank.GetComponent<TankData>().isAlive)
        aicount++;

      if(aicount >= instance.aiTanks.Count)
        enemiesDefeated = true;
      }

    /** Check Players defeated. */
    int playercount = 0;
    foreach(GameObject player in instance.playerTanks)
      {
      if(!player.GetComponent<TankData>().isAlive)
        playercount++;

      if(!multiPlayer && playercount >= 1)
        playersDefeated = true;
      else if(multiPlayer && playercount >= 2)
        playersDefeated = true;
      }

    gameOver = enemiesDefeated || playersDefeated;

    /** Game Over. */
    if(gameOver)
      {
      showGameOverMenu();
      }
    }

  /****************************************************************************
  * loadHighScore */
  /**
  * Load high score.
  ****************************************************************************/
  public void loadHighScore()
    {
    instance.highScore = PlayerPrefs.GetInt("HighScore");
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
  * loadSettings */
  /**
  * Load options settings.
  ****************************************************************************/
  public void loadSettings()
    {
    instance.mapSeed = PlayerPrefs.GetInt("MapSeed");
    setSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
    setMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
    instance.mapOfTheDay = PlayerPrefs.GetInt("MapOfTheDay") == 1;
    instance.multiPlayer = PlayerPrefs.GetInt("Multiplayer") == 1;
    }

  /****************************************************************************
  * saveHighScore */
  /**
  * Saves the high score.
  ****************************************************************************/
  public void saveHighScore()
    {
    PlayerPrefs.SetInt("HighScore", instance.highScore);
    PlayerPrefs.Save();
    }

  /****************************************************************************
  * saveSettings */
  /**
  * Saves the settings from the options menu.
  ****************************************************************************/
  public void saveSettings()
    {
    float sfxVolume;
    float musicVolume;
    instance.masterAudioMixer.GetFloat("sfxVolume", out sfxVolume);
    instance.masterAudioMixer.GetFloat("musicVolume", out musicVolume);

    PlayerPrefs.SetInt("MapSeed", instance.mapSeed);
    PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    PlayerPrefs.SetInt("MapOfTheDay", instance.mapOfTheDay ? 1 : 0);
    PlayerPrefs.SetInt("Multiplayer", instance.multiPlayer ? 1 : 0);
    PlayerPrefs.Save();
    }

  /****************************************************************************
  * setMusicVolume */
  /**
  * Sets the music volume.
  * @param  volume  Value to set volume to.
  ****************************************************************************/
  public void setMusicVolume(float volume)
    {
    instance.masterAudioMixer.SetFloat("musicVolume", volume);
    }

  /****************************************************************************
  * setSFXVolume */
  /**
  * Sets the sound effects volume.
  * @param  volume  Value to set volume to.
  ****************************************************************************/
  public void setSFXVolume(float volume)
    {
    instance.masterAudioMixer.SetFloat("sfxVolume", volume);
    }

  /****************************************************************************
  * loadGameOverScene */
  /**
  * Loads the Game Over Scene.
  ****************************************************************************/
  public void showGameOverMenu()
    {
    GameManager.instance.calculateHighScore();
    GameObject g = GameObject.Find("GameSceneManager");
    g.GetComponent<GameSceneManager>().gameOverMenu.SetActive(true);
    }
  }
