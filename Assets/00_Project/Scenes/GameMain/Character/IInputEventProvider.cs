using UnityEngine;
using UniRx;

namespace Project.Character.Input
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<bool> Attack { get; }
        IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
    }

}
