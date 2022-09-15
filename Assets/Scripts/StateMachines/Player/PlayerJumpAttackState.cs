using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : PlayerBaseState
{
    private readonly int JumpAttackHash = Animator.StringToHash("JumpAttack");

    private const float CrossFadeDuration = 0.1f;
    public PlayerJumpAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(-stateMachine.JumpForce);
        stateMachine.CurrentWeapon.gameObject.SetActive(true);
        stateMachine.Animator.CrossFadeInFixedTime(JumpAttackHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (stateMachine.Animator.IsInTransition(0) || stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CurrentWeapon.gameObject.SetActive(false);
    }


}
