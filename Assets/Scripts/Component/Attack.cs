using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Identity
{
    /* ------------------------------------------ */

    public Dictionary<Character, int> ListTempList;

    public Character Target;

    public int TargetEval;

    public int PreviousTargetEval;

    public string AnimationStateName;

    public RuntimeAnimatorController Controller;

    public bool IsRunning;

    public int Range;

    public bool WasThePreviousSequenceAttack;

    public GameObject ProjectileParent, ProjectileArrow;

    /* ------------------------------------------ */

    WaitForSeconds _delay;

    Stats _stats;

    Merge _merge;

    Drag _drag;

    float _distance = 999f;

    /* ------------------------------------------ */

    private void Awake()
    {
        ComponentManager.instance.Attack.Add(identity, this);
        _stats = GetComponent<Stats>();
        if (_stats.identity is PlayerCharacter)
            _merge = GetComponent<Merge>();
        _delay = new WaitForSeconds(0.1f);
    }

    void Start()
    {
        Target = null;
        ListTempList = new Dictionary<Character, int>();
    }


    private void OnDestroy()
    {
        try
        {
            if (ComponentManager.instance.Attack.ContainsKey(identity))
                ComponentManager.instance.Attack.Remove(identity);
        }
        catch { }

    }

    /* ------------------------------------------ */

    public override void Process()
    {
        Debug.Log("Attack Process");

        StartCoroutine(IEProcess());
    }

    IEnumerator IEProcess()
    {
        Debug.Log("Attack IEProcess");

        IsRunning = true;
        Target = null;

        PointTargetSystem();

        if (Target)
        {
            identity.Animator.Play(AnimationStateName);

            //Wait till animation ends
            yield return _delay;

            while (identity.Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationStateName))
                yield return _delay;

            //Set Damage
            if (this.identity.Type != UnitType.Ranger)
            {
                if (Target)
                    Target.gameObject.GetComponent<TakeDamage>().SetDamage(_stats.Data.Attack);
            }
            else
                ThrowAProjectile();
        }
        IsRunning = false;
    }

    /* ------------------------------------------ */

    public void PointTargetSystem()
    {
        Target = null;
        PreviousTargetEval = TargetEval;
        TargetEval = 0;
        List<Character> _enemyCharacters = ComponentManager.instance.EnemyCharacters;
        List<Character> _playerCharacters = ComponentManager.instance.PlayerCharacters;

        if (this._stats.identity is PlayerCharacter)
        {
            if (ComponentManager.instance.EnemyCharacters.Count > 0)
            {
                for (int i = 0; i < _enemyCharacters.Count; i++)
                {
                    int points = 0;
                    points += EvalDist(_enemyCharacters[i].transform.position);
                    points += EvalCharType(_enemyCharacters[i]);
                    KeyValuePair<Character, int> thisEnemy = new KeyValuePair<Character, int>(_enemyCharacters[i], points);
                    ListTempList.Add(_enemyCharacters[i], points);
                }
                foreach (KeyValuePair<Character, int> listItem in ListTempList)
                {
                    if (listItem.Value > TargetEval)
                    {
                        TargetEval = listItem.Value;
                        Target = listItem.Key;
                    }
                }
            }
            else
                Target = null;
        }
        else if (this._stats.identity is EnemyCharacter)
        {
            if (ComponentManager.instance.PlayerCharacters.Count > 0)
            {
                for (int i = 0; i < _playerCharacters.Count; i++)
                {
                    int points = 0;
                    points += EvalDist(_playerCharacters[i].transform.position);
                    points += EvalCharType(_playerCharacters[i]);
                    KeyValuePair<Character, int> thisEnemy = new KeyValuePair<Character, int>(_playerCharacters[i], points);
                    ListTempList.Add(_playerCharacters[i], points);
                }
                foreach (KeyValuePair<Character, int> listItem in ListTempList)
                {
                    if (listItem.Value > TargetEval)
                    {
                        TargetEval = listItem.Value;
                        Target = listItem.Key;
                    }
                }
            }
            else
                Target = null;
        }
        ListTempList.Clear();
    }

    public void ChangeAnimation()
    {
        identity.Animator.runtimeAnimatorController = Controller;
        identity.Animator.Play(AnimationStateName);
    }

    public bool IsItCloseEnoughToAttack()
    {
        if (Target)
        {
            if (Vector3.Distance(this.transform.position, Target.transform.position) <= (Range ))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public bool CanHaveTarget()
    {
        if (_stats.identity is PlayerCharacter && ComponentManager.instance.EnemyCharacters.Count > 0)
            return true;
        else if (_stats.identity is EnemyCharacter && ComponentManager.instance.PlayerCharacters.Count > 0)
            return true;
        else
            return false;
    }

    public void ThrowAProjectile()
    {
        Vector3 targetPos = Target.transform.position;

        var prefab = Instantiate(ProjectileArrow, ProjectileParent.transform).GetComponent<ProjectileMove>();
        prefab.Setup(targetPos, this, _stats);
    }

    /* ------------------------------------------ */
    private int EvalDist(Vector2 enemyPos)
    {
        float disX = Mathf.Abs(gameObject.transform.position.x - enemyPos.x);
        float disY = Mathf.Abs(gameObject.transform.position.y - enemyPos.y);
        if (disX < 0.5f)
            disX = 1;
        if (disY < 0.5f)
            disY = 1;
        int XEval = (int)(100 / disX);
        int YEval = (int)(10 / disY);
        int x = XEval + YEval;
        return x;
    }

    private int EvalCharType(Character enemy)
    {
        int x = 0;
        switch (this._stats.Data.Type)
        {
            default:
                break;
            case UnitType.Warrior:
                switch (enemy.GetComponent<Stats>().identity.Type)
                {
                    default:
                        x = 0;
                        break;
                    case UnitType.Warrior:
                        x = 4;
                        break;
                    case UnitType.Ranger:
                        x = 2;
                        break;
                }
                break;
            case UnitType.Ranger:
                switch (enemy.GetComponent<Stats>().identity.Type)
                {
                    default:
                        x = 0;
                        break;
                    case UnitType.Warrior:
                        x = 4;
                        break;
                    case UnitType.Ranger:
                        x = 2;
                        break;
                }
                break;
        }
        return x;
    }


   
}
