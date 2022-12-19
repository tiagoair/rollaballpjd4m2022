using UnityEngine;

namespace UnityTemplateProjects
{
    public class EnemyReturnBehaviour : StateMachineBehaviour
    {
        private EnemyController _myEnemyController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _myEnemyController = animator.gameObject.GetComponent<EnemyController>();
            
            _myEnemyController.SetDistance(_myEnemyController.distanceToFollow);
            
            Debug.Log(_myEnemyController.name + " entrou no estado Return.");
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Debug.Log(_myEnemyController.name + " saiu do estado Return.");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _myEnemyController.SetDestinationToPatrolRoute();
            _myEnemyController.CheckPatrolPointDistance();
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