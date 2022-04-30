using UnityEngine;

namespace State.Character
{
    public abstract class CharacterBaseState
    {

        /* ------------------------------------------ */

        public bool IsRunning;

        public abstract Type Type { get; }

        /* ------------------------------------------ */

        public abstract void EnterState(CharacterStateManager manager, Argument argument);

        public abstract void UpdateState();

        public abstract void FinishState(Type nextState, Argument argument);

        /* ------------------------------------------ */

    }
}
namespace State.Character
{
    public enum Type
    {
        None = 0,
        Idle = 1,
        Move = 2,
        Attack = 3,
        Death = 4,
        Spawn = 5,
        Win = 6,
        Drag=7
    }

    public struct Argument
    {
        public GameObject Character;
    }
}