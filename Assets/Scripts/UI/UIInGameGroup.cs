using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInGameGroup : MonoBehaviour
{
    public Button BtnWarrior, BtnRanger, BtnAttack;

    public Scrollbar PlayerHealthBar, EnemyHealthBar;

    private void Awake()
    {
        BtnWarrior.onClick.AddListener(() => FunWarrior());
        BtnRanger.onClick.AddListener(() => FunRanger());
        BtnAttack.onClick.AddListener(() => FunAttack());
    }

    public void FunWarrior() 
    {
        GameManager.instance.InstantiatePlayer(GameManager.instance.WarriorPlayers[0]);
    }

    public void FunRanger() 
    {
        GameManager.instance.InstantiatePlayer(GameManager.instance.RangerPlayers[0]);
    }

    public void FunAttack() 
    {
        StateManager.instance.ChangeStateAttack();
    }
}
