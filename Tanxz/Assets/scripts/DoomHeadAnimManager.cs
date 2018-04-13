using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* DoomHeadAnimManager */
/**
* Manages the Doom Head sprite animation. Consider using Animations.
******************************************************************************/
public class DoomHeadAnimManager : MonoBehaviour
  {
  /** Current Idle animation. */
  protected Sprite[] mCurrentIdleAnim;

  /** Tank Data reference. */
  public TankData tankData;

  /** Time before next sprite. */
  protected float mTransitionTime = 0.75f;

  /** States. */
  public enum AnimState { Idle, Shooting, Hit, Dead };

  [Header("Animation States")]
  /** Timer befor transition. */
  public float animationTimer;

  /** Current state. */
  public AnimState state = AnimState.Idle;

  [Header("Sprites")]
  /** Renders sprite. */
  public SpriteRenderer spriteRenderer;

  /** Sprite when hit. */
  public Sprite dead;

  /** Sprite when hit. */
  public Sprite hit1;
  public Sprite hit2;
  public Sprite hit3;

  /** Sprite when firing. */
  public Sprite shooting1;
  public Sprite shooting2;
  public Sprite shooting3;

  /** List of sprites for Idle state. */
  public Sprite[] idle1;
  public Sprite[] idle2;
  public Sprite[] idle3;

  /** Index in the idle animation. */
  public int idleIndex = 1;

  /****************************************************************************
  * Unity Methods 
  ****************************************************************************/
  /****************************************************************************
  * Start */
  /**
  ****************************************************************************/
  void Start() 
    {
    tankData = GetComponentInParent<TankData>();
    }

  /****************************************************************************
  * Update */
  /**
  ****************************************************************************/
  void Update ()
    {
    float threshold1 = 0.75f;
    float threshold2 = 0.5f;

    float hpPercentage = (float)tankData.hp / (float)tankData.baseHP;
    Debug.Log("hp% " + hpPercentage);

    if(!tankData.isAlive)
      changeToDeadState();

    /** Change animation. */
    /** Idle animation. */
    if(state == AnimState.Idle)
      {
      mCurrentIdleAnim = hpPercentage > threshold1 ? idle1 : hpPercentage > threshold2 ? idle2 : idle3;

      animationTimer -= Time.deltaTime;
      spriteRenderer.sprite = mCurrentIdleAnim[idleIndex];

      if(animationTimer <= 0f)
        {
        idleIndex = Random.Range(0, mCurrentIdleAnim.Length);
        animationTimer = mTransitionTime;
        }
      }

    /** Hit animation. */
    else if(state == AnimState.Hit)
      {
      animationTimer -= Time.deltaTime;
      spriteRenderer.sprite = hpPercentage > threshold1 ? hit1 : hpPercentage > threshold2 ? hit2 : hit3;
      if(animationTimer <= 0f)
        {
        changeToIdleState();
        animationTimer = mTransitionTime;
        }
      }

    /** Shooting animation. */
    else if(state == AnimState.Shooting)
      {
      animationTimer -= Time.deltaTime;
      spriteRenderer.sprite = hpPercentage > threshold1 ? shooting1 : hpPercentage > threshold2 ? shooting2 : shooting3;
      if(animationTimer <= 0f)
        {
        changeToIdleState();
        animationTimer = mTransitionTime;
        }
      }

    /** Dead animation. */
    else if(state == AnimState.Dead)
      {
      spriteRenderer.sprite = dead;
      animationTimer = mTransitionTime;
      }
    }

  /****************************************************************************
  * Methods 
  ****************************************************************************/
  /****************************************************************************
  * changeToHitState */
  /**
  * Transitions to Hit state.
  ****************************************************************************/
  public void changeToHitState()
    {
    state = AnimState.Hit;
    animationTimer = mTransitionTime;
    }

  /****************************************************************************
  * changeToIdleState */
  /**
  * Transitions to Idle state.
  ****************************************************************************/
  public void changeToIdleState()
    {
    state = AnimState.Idle;
    }

  /****************************************************************************
  * changeToShootingState */
  /**
  * Transitions to Shooting state.
  ****************************************************************************/
  public void changeToShootingState()
    {
    state = AnimState.Shooting;
    animationTimer = mTransitionTime;
    }

  /****************************************************************************
  * changeToDeadState */
  /**
  * Transitions to Dead state.
  ****************************************************************************/
  public void changeToDeadState()
    {
    state = AnimState.Dead;
    animationTimer = mTransitionTime;
    }
  }
