using UnityEngine;

namespace State.Character
{
    public class WinState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Win; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;

        Attack _attack;

        Stats _stats;

        bool _packItUp, _init;

        float _timer;

        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            
        }

        public override void UpdateState()
        {
           
        }

        public override void FinishState(Type nextState, Argument argument)
        {
            
        }

        /* ------------------------------------------ */

    }
}