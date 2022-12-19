using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityTemplateProjects;

public class EnemyController : MonoBehaviour
{
    public PatrolRouteManager myPatrolRoute;
    
    private SphereCollider _distanceToCheck;
    
    private Animator _enemyFSM;

    private NavMeshAgent _navMeshAgent;

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

    private Transform _playerTransform;

    private int _currentPatrolIndex;

    private Transform _currentPatrolPoint;

    private void Awake()
    {
        _enemyFSM = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _distanceToCheck = GetComponent<SphereCollider>();

        _name = myEnemyData.name;
        _maxHealth = myEnemyData.maxHealth;
        _moveSpeed = myEnemyData.moveSpeed;
        _attackDamage = myEnemyData.attackDamage;
        //_model = Instantiate e outras coisas: myEnemyData.model;
        distanceToFollow = myEnemyData.distanceToFollow;
        distanceToAttack = myEnemyData.distanceToAttack;
        distanceToLeaveFollow = myEnemyData.distanceToLeaveFollow;
        distanceToLeaveAttack = myEnemyData.distanceToLeaveAttack;
    }

    private void Start()
    {
        _currentPatrolIndex = 0;
        _currentPatrolPoint = myPatrolRoute.patrolPoints[_currentPatrolIndex];
    }

    public void SetDestinationToPatrolRoute()
    {
        _navMeshAgent.SetDestination(_currentPatrolPoint.position);
    }

    public void CheckPatrolPointDistance()
    {
        _enemyFSM.SetFloat("Distance", Vector3.Distance(transform.position, _currentPatrolPoint.position));
    }

    public void SetDistance(float distanceToSet)
    {
        _distanceToCheck.radius = distanceToSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyFSM.SetTrigger("Enter");
            Debug.Log("Player entrou");
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
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
}
