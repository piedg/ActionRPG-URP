using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    private readonly int HurtingHash = Animator.StringToHash("Hurt_1");
    private const float CrossFadeduration = 0.1f;

    private float duration = 1f;
    private float previousFrameTime;

    public EnemyHurtState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Health.OnTakeDamage += TakeDamageAgain;
        stateMachine.Animator.CrossFadeInFixedTime(HurtingHash, CrossFadeduration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit() {
        stateMachine.Health.OnTakeDamage -= TakeDamageAgain;
    }

    void TakeDamageAgain()
    {
        stateMachine.Animator.PlayInFixedTime(Animator.StringToHash("Hurt_2"));
    }
}
