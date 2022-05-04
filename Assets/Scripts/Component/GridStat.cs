using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public GridType Type;

    public int ID;

    public float X, Y;

    public List<GameObject> Players= new List<GameObject>();

    Stats _stats;

    CharacterStateManager _manager;

    public bool IsItFull=false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Type == GridType.player)
        {
            if (other.tag == "Player")
            {

                Players.Add(other.gameObject);
                _stats = Players[Players.Count - 1].GetComponent<Stats>();
                _manager = _stats.gameObject.GetComponent<CharacterStateManager>();
                _manager.GridStats.Add(this);
                other.gameObject.transform.position = this.transform.position;

                IsItFull = true;
            }
        }
        else if (Type == GridType.enemy) 
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.transform.position = this.transform.position;
                IsItFull = true;
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        { 
            other.gameObject.transform.position = this.transform.position;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            IsItFull = false;

            Players.Remove(other.gameObject);
        }
    }
}

public enum GridType 
{
    player,
    enemy
}
