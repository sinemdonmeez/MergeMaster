using UnityEngine;

namespace State.Character
{
    public class DeathState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Attack; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;

        Death death;

        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            _manager = manager;
            death = _manager.GetComponent<Death>();
        }

        public override void UpdateState()
        {
            death.Process();
        }

        public override void FinishState(Type nextState, Argument argument)
        {
            
        }

        /* ------------------------------------------ */

    }
}