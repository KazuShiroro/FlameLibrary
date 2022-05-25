using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Character.Player.Input;

namespace Project.Character.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimtor : BaseCharacterComponent
    {
        private Animator animator;
        public Animator Animator { get { return animator; } }

        new private Rigidbody rigidbody;

        public PlayerAnimatorPropertyNames animatorPropNames = new PlayerAnimatorPropertyNames();
        new private PlayerInputEventProvider InputEventProvider { get { return (PlayerInputEventProvider)base.InputEventProvider; } }

        protected override void OnInitialize()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            var playerMover = GetComponent<PlayerMover>();

            this.UpdateAsObservable()
                .Subscribe(__ =>
                {
                    InputEventProvider.MoveDirection.Subscribe(x => Move(x.magnitude));
                    playerMover.IsGrounded.Subscribe(x => IsGrounded(x));
                    InputEventProvider.Attack.Subscribe(x => Attack());
                    Falling(rigidbody.velocity.y);
                });
        }

        void Move(float speed)
        {
            animator.SetFloat(animatorPropNames.speed, speed, 0.1f, Time.deltaTime);
        }

        void IsGrounded(bool isGrounded)
        {
            animator.SetBool(animatorPropNames.isGrounded, isGrounded);
        }

        void Falling(float speed)
        {
            animator.SetFloat(animatorPropNames.fallSpeed, speed);
        }

        void Attack()
        {
            animator.SetTrigger(animatorPropNames.attack);
        }
    }

    [Serializable]
    public class PlayerAnimatorPropertyNames
    {
        public string speed;
        public string isJumping;
        public string isGrounded;
        public string fallSpeed;
        public string attack;
    }
}
