using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jumping");

    private const float CrossFadeDuration = 0.1f;


    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputManager.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerJumpAttackState(stateMachine));
            return;
        }
        Vector3 momentum;
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        Move(momentum, deltaTime);
        if (stateMachine.Controller.velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        
    }

    public override void Exit() { }
}