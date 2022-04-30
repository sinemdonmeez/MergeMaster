using UnityEngine;

namespace State.Character
{
    public class DragState : CharacterBaseState
    {

        /* ------------------------------------------ */

        public override Type Type { get => Type.Idle; }

        /* ------------------------------------------ */

        CharacterStateManager _manager;
        Drag _drag;

        bool _packItUp, _init;

        float _timer;

        public override void EnterState(CharacterStateManager manager, Argument argument)
        {
            _init = true;
            _manager = manager;
            _drag = _manager.Character.gameObject.GetComponent<Drag>();
            _manager = manager;
            _drag.ChangeAnimation();
        }

        public override void UpdateState()
        {
            if (_init) 
            {
                _drag.Process();
            }
  
        }

        public override void FinishState(Type nextState, Argument argument)
        {
            IsRunning = false;
            _packItUp = true;
            if (nextState != Type.None)
            {
                _manager.SwitchState(nextState, argument);
            }
        }

        /* ------------------------------------------ */

    }
}