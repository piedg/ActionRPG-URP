using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDumpTime = 0.1f;
    private const float CrossFadeDuration = 0.3f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputManager.JumpEvent += OnJump;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
       
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
     
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();


        /*if (!stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        } */

        if (stateMachine.InputManager.IsAttacking && stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0, movement));
            return;
        }

        Move(movement * stateMachine.MovementSpeed, deltaTime);

        if (stateMachine.InputManager.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDumpTime, deltaTime);
            return;
        } 

   

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, stateMachine.Controller.velocity.magnitude, AnimatorDumpTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
       stateMachine.InputManager.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

}
