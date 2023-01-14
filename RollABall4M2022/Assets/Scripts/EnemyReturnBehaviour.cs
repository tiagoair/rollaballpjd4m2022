<<<<<<< HEAD
using UnityEngine;

namespace UnityTemplateProjects
=======
﻿using UnityEngine;

>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
{
    public class EnemyReturnBehaviour : StateMachineBehaviour
    {
        private EnemyController _myEnemyController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _myEnemyController = animator.gameObject.GetComponent<EnemyController>();
            
            _myEnemyController.SetDistance(_myEnemyController.distanceToFollow);
            
<<<<<<< HEAD
            Debug.Log(_myEnemyController.name + " entrou no estado Return.");
=======
            Debug.Log(_myEnemyController.name + "entrou no estado Return.");
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
<<<<<<< HEAD
            Debug.Log(_myEnemyController.name + " saiu do estado Return.");
=======
            Debug.Log(_myEnemyController.name + "saiu do estado Return.");
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
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