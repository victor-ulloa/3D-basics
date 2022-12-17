using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CheckForPlayer), typeof(Enemy))]

public class FollowPlayer : MonoBehaviour {

    Enemy enemy;    
    CheckForPlayer sight;

    [SerializeField] float moveSpeed = 0;
    [SerializeField] bool isIdle;
    [SerializeField] bool isGhost;

    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start() {
        try {
            enemy = GetComponent<Enemy>();
            sight = GetComponent<CheckForPlayer>();

            if (moveSpeed <= 0.0f) {
                moveSpeed = 6.0f;
                throw new ArgumentNullException("Movespeed Argument is null so the value has been defaulted to 6.0");
            }
        } catch (ArgumentNullException e) {
            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void Update() {
        if (sight.isLookingAtPlayer) {
            playerPosition = sight.playerTransform.position;
            float angle = Vector3.Angle(sight.playerTransform.forward, transform.position - sight.playerTransform.position);
            if (angle > 50 && (Mathf.Abs(playerPosition.x - transform.position.x) > 3 || Mathf.Abs(playerPosition.z - transform.position.z) > 3)) {
                enemy.currentState = Enemy.EnemyState.Chase;
            } else if (isGhost) {
                enemy.currentState = Enemy.EnemyState.Idle ;
            } else {
                enemy.currentState = isIdle ? Enemy.EnemyState.Idle : Enemy.EnemyState.Patrol;
            }

        } else {
            enemy.currentState = isIdle ? Enemy.EnemyState.Idle : Enemy.EnemyState.Patrol;
            
            playerPosition.x = 0;
            playerPosition.z = 0;
        }
    }
}
