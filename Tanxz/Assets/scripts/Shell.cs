using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* Shell */
/**
* Represents a shell launched by a cannon.
******************************************************************************/
public class Shell : MonoBehaviour
  {
  [System.NonSerialized]
  /** Damage done by the shell. Should be set when fired. */ public int damage = 0;
  
  }
