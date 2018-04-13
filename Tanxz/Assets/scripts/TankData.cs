using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* TankData */
/**
* Holds all of the Tank's vital data.
******************************************************************************/
public class TankData : BaseData
  {
  /** Cannon launch force. */
  public float cannonForce = 5000f;

  /** Camera. */
  public Camera tankCam;

  /** Lowest amount of starting health. */
  public int baseHP = 100;

  /** Field of view angle. */
  public float fieldOfView = 45f;

  /** Delay between shots in seconds. */
  public float firingDelay = .5f;

  /** God Mode. */
  public bool godMode = false;

  /** Hit points. */
  public int hp = 100;

  /** Player number. -1 Denotes NPC. */
  public int playerNumber = -1;

  /** Listening radius. */
  public float listeningRadius = 50f;

  /** Meters/second. */
  public float moveSpeed = 4f;

  /** Point value for destroying tank. */
  public int pointValue = 100;

  /** Remaining lives. */
  public int remainingLives = 3;

  /** Tank's score. */
  public int score = 0;

  /** Shell damage. */
  public int shellDamage = 34;

  /** Tank's name. */
  public string tankName = "";

  /** Degrees/second. */
  public float turnSpeed = 180f;

  /** Range can see. */
  public float viewDistance = 100f;

  /****************************************************************************
  * Sounds
  ****************************************************************************/
  public AudioSource audioSource;
  public AudioClip   soundFiring;
  public AudioClip   soundTankHit;
  public AudioClip   soundTankDestroyed;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Awake */
  /**
  ****************************************************************************/
  void Awake()
    {
    hp = baseHP;

    /** Get the audio source. */
    if(audioSource == null)
      audioSource = gameObject.GetComponent<AudioSource>();

    /** Adjust Camera if multiplayer. AI should not have cameras. */
    if(GameManager.instance.multiPlayer && tankCam != null)
      {
      /** Adjust the camera to make split screen. */
      if(playerNumber == 1)
        tankCam.rect = new Rect(0f, .5f, 1f, .5f);
      if(playerNumber == 2)
        tankCam.rect = new Rect(0f, 0f, 1f, .5f);
      
      /** Adjust the UI so it is viewable by the player. */
      PlayerUIManager p = gameObject.GetComponent<PlayerUIManager>();

      Transform     dhT     = p.doomHead.transform;
      RectTransform hpRT    = p.txtHP.gameObject.GetComponent<RectTransform>();
      RectTransform scoreRT = p.txtScore.gameObject.GetComponent<RectTransform>();
      RectTransform livesRT = p.txtLives.gameObject.GetComponent<RectTransform>();

      float newDHY    = dhT.localPosition.y * .4f;
      float newDHX    = dhT.localPosition.x - 875f;
      float newHPY    = hpRT.localPosition.y * .3f;
      float newScoreY = scoreRT.localPosition.y * .45f;
      float newLivesY = livesRT.localPosition.y * .5f;

      dhT.localPosition     = new Vector3(newDHX, newDHY, dhT.position.z);
      hpRT.localPosition    = new Vector2(hpRT.localPosition.x,    newHPY);
      scoreRT.localPosition = new Vector2(scoreRT.localPosition.x, newScoreY);
      livesRT.localPosition = new Vector2(livesRT.localPosition.x, newLivesY);
      }
    }
  }
