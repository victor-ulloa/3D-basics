using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] Animator animator;

    private void Start() {
        if (damage <= 0) {
            damage = 1;
            throw new ArgumentNullException("damage Argument is not valid so the value has been defaulted to 1");
        }
    }

    private void OnTriggerEnter(Collider other) {
        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        
        if (other.gameObject.tag == "Enemy") {
            if (currentAnimation.IsName("PunchingRight") ||
                currentAnimation.IsName("KickRight")) {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.health -= damage;
            }
        }
    }
}
