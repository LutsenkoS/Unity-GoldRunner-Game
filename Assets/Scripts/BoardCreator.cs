using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TileType
{
    Floor, Wall
}
public class BoardCreator : MonoBehaviour {

    public GameObject floor;
    public GameObject wall;
    public int width;
    public int height;
    //public int minWallCount;
    //public int maxWallCount;
    [HideInInspector]
    public int[,] path;
    private TileType[][] tiles;
    private GameObject boardHolder;
    
    void Start()
    {
        boardHolder = GameObject.FindGameObjectWithTag("Board");
        SetupTilesArray();
        CreateFloor();
        CreateWalls();

        InstantiateTiles();
        InstantiateOuterWall();
        path = new int[width + 2, height + 2];
        path = GeneratePath();
        //if (Time.timeScale == 0)
        //    Time.timeScale = 1;
        //GameManager.Instance.score = 0;
    }

    private void InstantiateOuterWall()
    {
        
        float startingX = 0f;
        float startingY = 0f;
        float endingX = width + 2f;
        float endingY = height + 2f;
        InstantiateHorizontalOuterWalls(startingX, startingY, endingX);
        InstantiateHorizontalOuterWalls(startingX, endingY, endingX);
        InstantiateVerticalOuterWalls(startingY, startingX, endingY);
        InstantiateVerticalOuterWalls(startingY, endingX, endingY);



    }

    private void InstantiateVerticalOuterWalls(float startingY, float XCoord, float endingY)
    {
        float currentY = startingY;
        while(currentY <= endingY)
        {
            InstantiateFromArray(wall, XCoord, currentY);
            currentY++;

        }
    }

    private void InstantiateHorizontalOuterWalls(float startingX, float YCoord, float endingX)
    {
        float currentX = startingX;
        while(currentX <= endingX)
        {
            InstantiateFromArray(wall, currentX, YCoord);
            currentX++;
        }
    }

    private void InstantiateTiles()
    {
        for (int i = 1; i < tiles.Length; i++)
            for (int j = 1; j < tiles[i].Length; j++)
            {
                InstantiateFromArray(floor, i, j);
                if (tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wall, i, j);
                }
            }
    }

    private void InstantiateFromArray(GameObject prefab,float x, float y)
    {
        float z = 0.5f;
        if (prefab == wall)
        {
            z = 0.0f;
        }         
        Vector3 position = new Vector3(x, y, z);
        GameObject tileInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        tileInstance.transform.parent = boardHolder.transform;
    }

    private void CreateWalls()
    {

        for (int i = 2; i < width + 1; i++)
        {
            RandomPosition(i);
            i++;
        }
    }

    private void RandomPosition(int XCoord)
    {
        int YCoord = UnityEngine.Random.Range(1, 3);
        while(YCoord <= height + 1)
        {
            int blockLength = (int)UnityEngine.Random.Range(1.0f, width / 4);
            //int startPos = UnityEngine.Random.Range(0, 2);
            for (int i = 0; i < blockLength; i++)
            {
                if (YCoord == height + 2)
                    break;
                tiles[XCoord][YCoord] = TileType.Wall;
                YCoord++;
            }
            YCoord++;
        }
    }

    //private void RandomPosition(int row)
    //{
    //    int wallCount = UnityEngine.Random.Range(minWallCount, maxWallCount);
    //    int[] positions = new int[wallCount];
    //    //int pos = UnityEngine.Random.Range(1, width);
    //    System.Random r = new System.Random(unchecked((int)DateTime.Now.Ticks));

    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        positions[i] = r.Next(width);
    //        //Debug.Log(positions[i].ToString() + " " + row.ToString());
    //    }
    //    foreach (int position in positions)
    //    {
    //        tiles[position][row] = TileType.Wall;
    //    }
    //}

    private void CreateFloor()
    {
        for (int i = 1; i < width; i++)
            for (int j = 1; j < height; j++)
            {
                tiles[i][j] = TileType.Floor;
            }
            
        
    }

    private void SetupTilesArray()
    {
        tiles = new TileType[width + 2][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[height + 2];
        }
    }

    private int[,] GeneratePath()
    {
        int[,] path = new int[width + 2, height + 2];
        for (int i = 0; i < width + 2; i++)
            for (int j = 0; j < height + 2; j++)
            {
                path[i, j] = (int)tiles[i][j];
            }

        return path;
    }
}
