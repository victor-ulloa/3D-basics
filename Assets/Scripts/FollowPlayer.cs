using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        } else {
            playerPosition.x = 0;
            playerPosition.z = 0;
        }
    }
}
