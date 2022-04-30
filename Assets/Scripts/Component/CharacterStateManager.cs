using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State.Character;

public class CharacterStateManager : MonoBehaviour
{

    /* ------------------------------------------ */

    [HideInInspector]
    public Character Character;

    public CharacterBaseState CurrentState;

    public Vector3 Offset;

    public List<GridStat> GridStats = new List<GridStat>();

    /* ------------------------------------------ */

    AttackState _attackState = new AttackState();
    IdleState _idleState = new IdleState();
    MoveState _moveState = new MoveState();
    DeathState _deathState = new DeathState();
    SpawnState _spawnState = new SpawnState();
    WinState _winState = new WinState();
    DragState _dragState = new DragState();


    WaitForSeconds _delay;

    Stats _stats;

    bool _dragControl;
    /* ------------------------------------------ */

    private void Awake()
    {
        Character = GetComponent<Character>();

        ComponentManager.instance.CharacterStateManager.Add(Character, this);

        StateManager.instance.CharacterStateManagers.Add(this);
    }

    private void Start()
    {
        _delay = new WaitForSeconds(.25f);

        _stats = GetComponent<Stats>();

        StateManager.instance.CharacterStateManagers.Add(this);

        if (Character.GetComponent<PlayerCharacter>())
        {
            _spawnState.EnterState(this, new Argument());
            CurrentState = _spawnState;
        }
        else
        {
            _idleState.EnterState(this, new Argument());
            CurrentState = _idleState;
        }
    }

    /* ------------------------------------------ */

    private void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateState();
    }

    private void OnMouseDown()
    {
        if(this.gameObject.tag=="Player")
            _dragControl = true;
    }

    private void OnMouseDrag()
    {
        if (this.gameObject.tag == "Player")
            SwitchState(Type.Drag);
    }

    private void OnMouseUp()
    {
        if (_dragControl) 
        {
            _dragControl = false;
            SwitchState(Type.Idle);
        }
    }


    /* ------------------------------------------ */
    private void OnDisable()
    {
        ComponentManager.instance.CharacterStateManager.Remove(Character);
        StateManager.instance.CharacterStateManagers.Remove(this);
    }
 

    /* ------------------------------------------ */

    IEnumerator IESwitchState(Type type, Argument argument, bool force)
    {
        //Check the character can run the state
        if (CheckIsItValidState(type))
        {
            Debug.Log("IESwitchState : " + type.ToString());

            //Make sure the character hasn't died
            if (_stats.Health > 0)
            {
                //Run the next state
                switch (type)
                {
                    case Type.Attack:
                        CurrentState = _attackState;
                        break;

                    case Type.Idle:
                        CurrentState = _idleState;
                        break;

                    case Type.Move:
                        CurrentState = _moveState;
                        break;

                    case Type.Death:
                        CurrentState = _deathState;
                        break;

                    case Type.Spawn:
                        CurrentState = _spawnState;
                        break;

                    case Type.Win:
                        CurrentState = _winState;
                        break;
                    case Type.Drag:
                        CurrentState = _dragState;
                        break;
                }
                CurrentState.EnterState(this, argument);

            }
            else
            {
                _deathState.EnterState(this, argument);
                CurrentState = _deathState;
            }

            yield return null;
        }
    }

    /* ------------------------------------------ */

    bool CheckIsItValidState(Type type)
    {
        switch (type)
        {
            case Type.Attack:

                if (!GetComponent<Attack>())
                    return false;

                break;

            case Type.Idle:

                if (!GetComponent<Idle>())
                    return false;

                break;

            case Type.Move:

                if (!GetComponent<Move>())
                    return false;

                break;

            case Type.Death:

                if (!GetComponent<Death>())
                    return false;

                break;

            case Type.Spawn:

                if (!GetComponent<Spawn>())
                    return false;

                break;

            case Type.Win:
                if (!GetComponent<Win>())
                    return false;
                break;

            case Type.Drag:
                if (!GetComponent<Drag>())
                    return false;

                break;
        }

        return true;
    }

    /* ------------------------------------------ */

    public void SwitchState(Type type)
    {
        if(gameObject)
            StartCoroutine(IESwitchState(type, new Argument(), false));
    }

    public void SwitchState(Type type, Argument argument)
    {
        if(gameObject)
            StartCoroutine(IESwitchState(type, argument, false));
    }

    /* ------------------------------------------ */

}
