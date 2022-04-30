using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public gridType Type;

    public int Rows = 4;

    public int Columns = 4;

    public float ScaleX = 1;

    public float ScaleZ = 1;

    public GameObject GridPrefab;

    public Vector3 LeftBottomLoc = Vector3.zero;

    private void Awake()
    {

            GeneratePlayerGrid();
 

    }
    void GeneratePlayerGrid()
    {
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                GameObject go = Instantiate(GridPrefab, this.transform);
                go.transform.position = new Vector3(LeftBottomLoc.x + ScaleX * i, LeftBottomLoc.y, LeftBottomLoc.z + ScaleZ * j);
                GridStat gridStat= go.GetComponent<GridStat>();
                if(Type==gridType.player)
                    GameManager.instance.PlayerGrid.Add(gridStat);
                gridStat.X = i;
                gridStat.Y =j;
                gridStat.ID = i + j;
            }
        }
    }
}

public enum gridType 
{
    player,
    enemy
}
