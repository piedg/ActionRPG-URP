using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Falling");

    private const float CrossFadeDuration = 0.1f;


    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputManager.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerJumpAttackState(stateMachine));
            return;
        }

        if (stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * (stateMachine.MovementSpeed / 2), deltaTime);

    }

    public override void Exit() { }


}