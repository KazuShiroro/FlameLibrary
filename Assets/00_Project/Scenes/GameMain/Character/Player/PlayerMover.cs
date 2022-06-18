using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Project.Character.Input;
using Project.Character.Player.Input;
using Flame.Unity;

namespace Project.Character
{
    namespace Player
    {
        [RequireComponent(typeof(CapsuleCollider))]
        [RequireComponent(typeof(Rigidbody))]
        public class PlayerMover : BaseCharacterComponent
        {
            [SerializeField] PlayerMoverInfo moverInfo = new PlayerMoverInfo();
            public PlayerMoverInfo MoverInfo => moverInfo;

            public CheckGroundedModule checkGrounded;

            new private Rigidbody rigidbody;

            public Rigidbody MyRigidbody { get { return rigidbody; } }
            new private PlayerInputEventProvider InputEventProvider { get { return (PlayerInputEventProvider)base.InputEventProvider; } }

            #region ReactiveProperties

            FloatReactiveProperty moveSpeed = new FloatReactiveProperty();
            BoolReactiveProperty isJumping = new BoolReactiveProperty();
            BoolReactiveProperty isGrounded = new BoolReactiveProperty();

            public IReadOnlyReactiveProperty<bool> IsJumping { get { return isJumping; } }
            public IReadOnlyReactiveProperty<bool> IsGrounded { get { return isGrounded; } }
            public IReadOnlyReactiveProperty<float> MoveSpeed { get { return moveSpeed; } }
            
            #endregion


            protected override void OnInitialize()
            {
                rigidbody = GetComponent<Rigidbody>();

                // 接地チェックのモジュールの初期化
                var collider = GetComponent<CapsuleCollider>();
                checkGrounded = new CheckGroundedModule(collider);

                // ジャンプ
                InputEventProvider.Jump
                    .ThrottleFirst(TimeSpan.FromSeconds(1f))
                    .Subscribe(_ =>
                    {
                        Jump(moverInfo.JumpHeight);
                    });

                // メインカメラ
                var mainCamera = Camera.main;

                // 移動処理
                InputEventProvider.MoveDirection
                    .Where(_=> !InputEventProvider.Attack.Value)
                    .Subscribe(x =>
                    {
                        var velocity = x * moverInfo.BaseMoveSpeed;
                        var horizontalRotation = Quaternion.AngleAxis(mainCamera.transform.eulerAngles.y, Vector3.up);
                        velocity = horizontalRotation * velocity; 

                        Move(velocity);
                        //MoveFps(velocity);

                        RotatePlayer((velocity).normalized);
                    });

                // 移動計算処理:FixedUpdate
                this.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        rigidbody.velocity = new Vector3(moverInfo.velocity.x, rigidbody.velocity.y, moverInfo.velocity.z);

                        isGrounded.Value = checkGrounded.CheckGrounded();
                    });
            }

            public void ApplyForce(Vector3 force)
            {
                Observable.NextFrame(FrameCountType.FixedUpdate)
                    .Subscribe(_ => rigidbody.AddForce(force, ForceMode.VelocityChange));
            }

            void Move(Vector3 velocity)
            {
                moverInfo.velocity = velocity;
                moveSpeed.Value = velocity.magnitude;

                Debug.Log($"x = {MyRigidbody.velocity.x}, y = {MyRigidbody.velocity.y}, z = {MyRigidbody.velocity.z}");
            }

            void MoveFps(Vector3 velocity)
            {
                var forward = (transform.forward * velocity.z);
                var right = (transform.right * velocity.x);

                velocity = (forward + right).normalized * moverInfo.BaseMoveSpeed;
                velocity.y = rigidbody.velocity.y;

                moverInfo.velocity = velocity;
                moveSpeed.Value = velocity.magnitude;
            }

            void RotatePlayer(Vector3 direction)
            {
                var lookRotation = transform.rotation;

                if (direction.magnitude > 0.5f)
                {
                    lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                }

                var rotationSpeed = moverInfo.RotationSpeed * Time.deltaTime;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed);
            }

            public void Jump(float power)
            {
                ApplyForce(Vector3.up * power);
                isJumping.Value = true;
            }

            public void Stop()
            {
                rigidbody.velocity = Vector3.zero;
                moverInfo.velocity = Vector3.zero;
            }
        }

        [Serializable]
        public class PlayerMoverInfo
        {
            [Tooltip("基本移動速度")]
            [SerializeField] float baseMoveSpeed = 2f;
            public float BaseMoveSpeed => baseMoveSpeed;

            [Tooltip("走行速度倍率")]
            [SerializeField] float sprintSpeedRate = 1.5f;
            public float SprintSpeedRate => sprintSpeedRate;

            [Tooltip("ジャンプの高さ")]
            [SerializeField] float jumpHeight = 1.2f;
            public float JumpHeight => jumpHeight;

            [Tooltip("回転速度")]
            [SerializeField] float rotationSpeed = 720f;
            public float RotationSpeed => rotationSpeed;

            [Tooltip("現在のVelocity")]
            public Vector3 velocity;
        }
    }

    [Serializable]
    public class CheckGroundedModule
    {
        [SerializeField] float checkDistance = -0.45f;
        [SerializeField] float checkRadius = 0.5f;

        CapsuleCollider collider;

        public CheckGroundedModule(CapsuleCollider collider)
        {
            this.collider = collider;
        }

        public bool CheckGrounded()
        {
            var center = collider.transform.position + collider.center;

            var maxDistance = collider.height / 2f + checkDistance;

            return CheckGrounded(center, maxDistance, checkRadius);
        }

        public bool CheckGrounded(Vector3 center, float ditance = 1f, float radius = 0.5f)
        {
            Ray ray = new Ray(center, Vector3.down);
            var result = Physics.SphereCast(ray, radius, ditance);

            return result;
        }
    }
}
