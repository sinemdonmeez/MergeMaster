using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public int ID;

    Stats _stats;

    CharacterStateManager _characterStateManager;
    // Start is called before the first frame update
    void Start()
    {
        ID = GetInstanceID();

        _stats = GetComponent<Stats>();

        _characterStateManager = GetComponent<CharacterStateManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player") 
        {
            Stats collisionStat = collision.gameObject.GetComponent<Stats>();
            if (ID > collision.gameObject.GetComponent<Merge>().ID)
            { 
                if (_stats.Data.Type == collisionStat.Data.Type)
                {
                    UnitType type = _stats.Data.Type;
                    if (_stats.Data.Level == collisionStat.Data.Level && _stats.Data.Level < 4) 
                    {
                        int level = _stats.Data.Level+1 ;
                        Vector3 pos = this.transform.position;
                        GameManager.instance.InstantiatePlayer(level, type).transform.position=pos;
                        StateManager.instance.CharacterStateManagers.Remove(_characterStateManager);
                        StateManager.instance.CharacterStateManagers.Remove(collision.gameObject.GetComponent<CharacterStateManager>());
                        Destroy(collision.gameObject);
                        Destroy(this.gameObject);
                        
                    }
                }
            }
            
        }
    }
}
