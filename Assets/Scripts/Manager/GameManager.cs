using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* ------------------------------------------ */

    public static GameManager instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }
    static GameManager _instance;

    /* ------------------------------------------ */

    public List<GridStat> PlayerGrid = new List<GridStat>();

    public GridBehaviour GridPlayer, GridEnemy;

    public List<GameObject> WarriorPlayers = new List<GameObject>();

    public List<GameObject> RangerPlayers=new List<GameObject>();
    
    public List<EnemySet> EnemySets = new List<EnemySet>();

    public List<Stats> WarriorPlayersStat = new List<Stats>();

    public List<Stats> RangerPlayersStat = new List<Stats>();

    /* ------------------------------------------ */

    bool _playerGridControl = false;

    public List<GridStat> enemyStats;

    private void Awake()
    {
        for (int i = 0; i < WarriorPlayers.Count; i++)
        {
            WarriorPlayersStat.Add(WarriorPlayers[i].GetComponent<Stats>());
        }
        for (int i = 0; i < RangerPlayers.Count; i++)
        {
            RangerPlayersStat.Add(RangerPlayers[i].GetComponent<Stats>());
        }
    }

    private void Start()
    {
        InstantiateEnemySet();
    }
    public void InstantiatePlayer(GameObject gameObject) 
    {
        Vector3 pos = new Vector3(-99,-99,-99);

        int final = GridPlayer.Rows * GridPlayer.Columns;
        
        for (int i = 0; i < final; i++)
        {
            int random = Random.Range(0, final);

            if (PlayerGrid[random].IsItFull == false) 
            {
                _playerGridControl = true;
                pos = PlayerGrid[random].transform.position;
                break;
            }
        }

        if (_playerGridControl)
        {
            Instantiate(gameObject, pos, Quaternion.identity);
            _playerGridControl = false;
        }
    }

    public GameObject InstantiatePlayer(int level, UnitType unitType) 
    {
        GameObject go = new GameObject() ;
        switch (unitType) 
        {
            case UnitType.Warrior:
                for(int i = 0; i < WarriorPlayers.Count; i++) 
                {
                    if (level == WarriorPlayersStat[i].Data.Level) 
                    {
                        go = WarriorPlayers[i];
                        break;
                    }
                }
                break;
            case UnitType.Ranger:
                for (int i = 0; i < RangerPlayers.Count; i++)
                {
                    if (level == RangerPlayersStat[i].Data.Level)
                    {
                        go = RangerPlayers[i];
                        break;
                    }
                }
                break;
        }
        return Instantiate(go, Vector3.zero, Quaternion.identity);
    }

    void InstantiateEnemySet() 
    {
        int random = Random.Range(0, EnemySets.Count);

        for (int i = 0; i < EnemySets[random].Prefabs.Count; i++) 
        {
            int random2;
            do
            {
                random2 = Random.Range(0, GridEnemy.transform.childCount);
            } while (GridEnemy.transform.GetChild(random2).GetComponent<GridStat>().IsItFull);
            
            Vector3 pos = GridEnemy.gameObject.transform.GetChild(random2).transform.position;
            Instantiate(EnemySets[random].Prefabs[i], pos, EnemySets[random].Prefabs[i].gameObject.transform.rotation);
        }
    }
}
