using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
* GridManager */
/**
* Manages the creation of the play area.
* 
* 3x3 Grid structure:
* [2,0],[2,1],[2,2]
* [1,0],[1,1],[1,2]
* [0,0],[0,1],[0,2]
******************************************************************************/
public class GridManager : MonoBehaviour
  {
  /** Chunk width. */
  public int chunkWidth;

  /** Chunk height. */
  public int chunkHeight;

  /** Number of Chunks accross. */
  public int rows;

  /** Number of Chunks down. */   
  public int cols;

  /** Chunk prefabs. */           
  public GameObject[] chunks;

  /** Game grid of Chunks. */     
  public Chunk[,] grid;

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
        float   xpos = chunkWidth  * c;
        float   zpos = chunkHeight * r;
        Vector3 pos  = new Vector3(xpos, 0f, zpos);

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
        else if (c == cols - 1)
          {
          chunk.wallWest.SetActive(false);
          }

        /** In the middle, drop East and West walls. */
        else
          {
          chunk.wallEast.SetActive(false);
          chunk.wallWest.SetActive(false);
          }

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
