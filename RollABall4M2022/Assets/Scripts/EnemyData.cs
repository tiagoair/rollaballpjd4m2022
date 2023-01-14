<<<<<<< HEAD
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> 5881a230993fc023e62fdd753b5ce76553509068

namespace UnityTemplateProjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Assets/Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public string name;

        public int maxHealth;

        public int moveSpeed;

        public int attackDamage;

        public GameObject model;

        public float distanceToFollow;

        public float distanceToAttack;

        public float distanceToLeaveAttack;

        public float distanceToLeaveFollow;
    }
}