using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using UniRx.Triggers;
using Project.Character.Input;
using Project.Character.Player;

namespace Project.Character.Player.Input
{
    public class PlayerInputEventProvider : BaseCharacterComponent, IInputEventProvider
    {
        private PlayerInputActions inputActions;

        private ReactiveProperty<bool> attack = new ReactiveProperty<bool>();
        private ReactiveProperty<Vector3> moveDirection = new ReactiveProperty<Vector3>();
        private ReactiveProperty<bool> jump = new ReactiveProperty<bool>();
        private ReactiveProperty<Vector2> lookRotation = new ReactiveProperty<Vector2>();

        public IReadOnlyReactiveProperty<bool> Attack { get { return attack; } }

        public IReadOnlyReactiveProperty<Vector3> MoveDirection { get { return moveDirection; } }

        public IReadOnlyReactiveProperty<bool> Jump { get { return jump; } }

        public IReadOnlyReactiveProperty<Vector2> LookRotation { get { return lookRotation; } }

        bool isRun;
        private void OnGUI()
        {
            GUILayout.Label($"{inputActions.Player.Look.ReadValue<Vector2>()}");
        }
        protected override void OnInitialize()
        {

            inputActions = new PlayerInputActions();
            inputActions.Enable();
             
            var playerMover = GetComponent<PlayerMover>();

            // Runボタン
            inputActions.Player.Run.performed += context => isRun = true;
            inputActions.Player.Run.canceled += context => isRun = false;

            // 移動の入力処理
            this.UpdateAsObservable()
                .Select(_ =>
                {
                    // InputManager(Old)
                    if(UnityEngine.Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("Ahoy!!");
                    }

                    /* InputSystem(New) */
                    // 入力方向の取得
                    var inputDirection = inputActions.Player.Move.ReadValue<Vector2>();

                    // 移動方向に変換
                    var moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

                    // trueならスプリント(走行)適用
                    if (isRun)
                    {
                        moveDirection = Run(moveDirection, playerMover.MoverInfo.SprintSpeedRate);
                    }

                    return moveDirection;
                })
                .Subscribe(x => moveDirection.SetValueAndForceNotify(x));

            this.UpdateAsObservable()
                .Subscribe(x =>
                {
                    var inputLookRotation = inputActions.Player.Look.ReadValue<Vector2>();
                    lookRotation.Value = inputLookRotation;
                });

            // 攻撃入力
            this.UpdateAsObservable()
                .Select(_ => inputActions.Player.Fire.triggered)
                .DistinctUntilChanged()
                .Subscribe(x => attack.Value = x);

            // ジャンプ入力
            this.UpdateAsObservable()
                .Select(_ => inputActions.Player.Jump.triggered)
                .Subscribe(x => jump.Value = x);
        }

        Vector3 Run(Vector3 direction, float rate)
        {
            return direction * rate;
        }

    }

}
