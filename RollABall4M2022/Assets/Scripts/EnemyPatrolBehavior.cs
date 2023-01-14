using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class EnemyPatrolBehaviour : StateMachineBehaviour
    {
        public EnemyController _myEnemyController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _myEnemyController = animator.gameObject.GetComponent<EnemyController>();
            _myEnemyController.SetDistance(_myEnemyController.distanceToFollow);
            Debug.Log(_myEnemyController.name + "Entrou no estado Patrol");
            _myEnemyController.SetDestinationToPatrolRoute();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Debug.Log(_myEnemyController.name + "saiu do estado Patrol.");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _myEnemyController.CheckPatrolPointDistance();
            float distance = _myEnemyController._enemyFSM.GetFloat("Distance");
            if (distance <= 0.5f)
            {
                _myEnemyController.UpdatePatrolPoint();
                _myEnemyController.SetDestinationToPatrolRoute();
            }


        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}
