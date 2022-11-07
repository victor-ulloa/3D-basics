using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CheckForPlayer))]

public class FollowPlayer : MonoBehaviour {
    

    CheckForPlayer sight;

    [SerializeField] float moveSpeed = 0;

    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start() {
        try {
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
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
            }
        } else {
            playerPosition.x = 0;
            playerPosition.z = 0;
        }
    }
}
