using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Character
{
    public class AttackStateBehaviour : StateMachineBehaviour
    {
        private ReactiveProperty<bool> attack = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> Attack { get { return attack; } }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            attack.Value = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            attack.Value = false;
        }
    }
}
