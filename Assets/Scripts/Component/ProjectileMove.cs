using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;
using System.Linq;

public class ProjectileMove : MonoBehaviour
{
    /* ------------------------------------------ */

    //public delegate void EndEventHandler(object sender, ProjectileEndEventArg arg);
    //public event EndEventHandler Event;

    /* ------------------------------------------ */

    public Vector3 TargetPos;

    public float Speed = 10;
    public float ArcHeight = 1;

    public float DestroyDelay;

    public bool LookAtIt;

    /* ------------------------------------------ */

    Attack _attack;
    Stats _stats;
    TakeDamage _takeDamage;
    int _counter;

    bool _init = false;
    bool _lock = false;

    int _damage;

    Vector3 _startPos;

    GameObject _tmpObject, _target;

    bool hasHitBefore = false;

    /* ------------------------------------------ */


    void Start()
    {
        _counter = 0;
        _startPos = transform.position;

        if (gameObject.GetComponent<Animator>())
        {
            gameObject.GetComponent<Animator>().SetBool("hit", false);
        }
    }

    /* ------------------------------------------ */

    void Update()
    {
        float x0 = _startPos.x;
        float x1 = TargetPos.x;
        float dist = x1 - x0;

        float nextX = Mathf.MoveTowards(transform.position.x, x1, Speed * Time.deltaTime);
        float baseY = Mathf.Lerp(_startPos.y, TargetPos.y, (nextX - x0) / dist);

        float arc = ArcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);

        var nextPos = new Vector3(nextX, baseY + arc, TargetPos.z);

        transform.position = nextPos;

        Debug.Log("next :" +nextPos);
        Debug.Log("target : " + TargetPos);

            if (nextPos == TargetPos) Arrived();
    }

    /* ------------------------------------------ */

    void Arrived()
    {
        if (_stats)
            Destroy(gameObject);
        else
        {
            if (!_target)
            {
                if (_counter == 0 && ComponentManager.instance.EnemyCharacters.Count > 0)
                {
                    foreach (Character enemy in ComponentManager.instance.EnemyCharacters.ToList())
                    {
                        enemy.GetComponent<TakeDamage>().SetDamage(_damage);
                    }
                    _counter++;
                }
            }
            else
                _target.GetComponent<TakeDamage>().SetDamage(_damage);

            Destroy(this.gameObject, DestroyDelay);

        }
    }

    /* ------------------------------------------ */

    public void Setup(Vector3 targetPos, Attack attack, Stats stats)
    {
        TargetPos = targetPos;

        _stats = stats;

        _attack = attack;

        _init = true;
    }

    /* ------------------------------------------ */

}
