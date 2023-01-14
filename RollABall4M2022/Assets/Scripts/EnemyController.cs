using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityTemplateProjects;

public class EnemyController : MonoBehaviour
{
    public PatrolRouteManager myPatrolRoute;
<<<<<<< HEAD
    
    private SphereCollider _distanceToCheck;

    internal Animator _enemyFSM;

    private NavMeshAgent _navMeshAgent;
=======

    private SphereCollider _distanceToCheck;
    
    public Animator _enemyFSM;

    public NavMeshAgent _navMeshAgent;
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068

    public EnemyData myEnemyData;
    
    private string _name;

    private int _maxHealth;

    private int _moveSpeed;

    private int _attackDamage;

    private GameObject _model;
    
    public float distanceToFollow;

    public float distanceToAttack;

    public float distanceToLeaveAttack;

    public float distanceToLeaveFollow;

<<<<<<< HEAD
=======
    public float pointToPatrol;

>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    private Transform _playerTransform;

    private int _currentPatrolIndex;

    private Transform _currentPatrolPoint;
<<<<<<< HEAD

=======
    
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    private void Awake()
    {
        _enemyFSM = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _distanceToCheck = GetComponent<SphereCollider>();
<<<<<<< HEAD

=======
        
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
        _name = myEnemyData.name;
        _maxHealth = myEnemyData.maxHealth;
        _moveSpeed = myEnemyData.moveSpeed;
        _attackDamage = myEnemyData.attackDamage;
<<<<<<< HEAD
        //_model = Instantiate e outras coisas: myEnemyData.model;
        distanceToFollow = myEnemyData.distanceToFollow;
        distanceToAttack = myEnemyData.distanceToAttack;
        distanceToLeaveFollow = myEnemyData.distanceToLeaveFollow;
        distanceToLeaveAttack = myEnemyData.distanceToLeaveAttack;
=======
        //_model = Instantiate e outras coisas: myEnemyData.model
        distanceToFollow = myEnemyData.distanceToFollow;
        distanceToAttack = myEnemyData.distanceToAttack;
        distanceToLeaveAttack = myEnemyData.distanceToLeaveAttack;
        distanceToLeaveFollow = myEnemyData.distanceToLeaveFollow;
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    }

    private void Start()
    {
        _currentPatrolIndex = 0;
        _currentPatrolPoint = myPatrolRoute.patrolPoints[_currentPatrolIndex];
    }

<<<<<<< HEAD
=======
    public void SetDistance(float distanceToSet)
    {
        _distanceToCheck.radius = distanceToSet;
    }

>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    public void SetDestinationToPatrolRoute()
    {
        _navMeshAgent.SetDestination(_currentPatrolPoint.position);
    }

    public void CheckPatrolPointDistance()
    {
<<<<<<< HEAD
        _enemyFSM.SetFloat("Distance", Vector3.Distance(transform.position, myPatrolRoute.patrolPoints[_currentPatrolIndex].position));
=======
        _enemyFSM.SetFloat("Distance", Vector3.Distance(transform.position, _currentPatrolPoint.position));
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    }

    public void SetDistance(float distanceToSet)
    {
        _distanceToCheck.radius = distanceToSet;
    }

<<<<<<< HEAD
    public void UpdatePatrolPoint()
    {
        _currentPatrolIndex++;
        if (_currentPatrolIndex >= myPatrolRoute.patrolPoints.Count)
        {
            _currentPatrolIndex = 0;
        }

        _currentPatrolPoint = myPatrolRoute.patrolPoints[_currentPatrolIndex];
    }

    private void OnTriggerEnter(Collider other)
=======
    public void OnTriggerEnter(Collider other)
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    {
        if (other.CompareTag("Player"))
        {
            _enemyFSM.SetTrigger("Enter");
            Debug.Log("Player entrou");
            _playerTransform = other.transform;
        }
    }

<<<<<<< HEAD
    private void OnTriggerExit(Collider other)
=======
    public void OnTriggerExit(Collider other)
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
    {
        if (other.CompareTag("Player"))
        {
            _enemyFSM.SetTrigger("Exit");
            Debug.Log("Player saiu");
        }
    }

    public void SetDestinationToPlayer()
    {
        _navMeshAgent.SetDestination(_playerTransform.position);
    }
<<<<<<< HEAD
=======
    
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068
}
