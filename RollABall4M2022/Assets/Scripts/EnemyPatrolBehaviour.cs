using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : StateMachineBehaviour
{
    private EnemyController _myEnemyController;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _myEnemyController = animator.gameObject.GetComponent<EnemyController>();
        _myEnemyController.SetDestinationToPatrolRoute();
        _myEnemyController.SetDistance(_myEnemyController.distanceToFollow);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _myEnemyController.CheckPatrolPointDistance();
        float distance = _myEnemyController._enemyFSM.GetFloat("Distance");

        if (distance <= 0.5f)
        {
            _myEnemyController.UpdatePatrolPoint();
            _myEnemyController.SetDestinationToPatrolRoute();
        }
        Debug.Log(_myEnemyController.name + " entrou no estado Patrol.");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(_myEnemyController.name + " saiu do estado Patrol.");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
