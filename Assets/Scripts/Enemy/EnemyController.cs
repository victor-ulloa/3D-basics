using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Animator anim;

    [SerializeField] int health = 5;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponentInChildren<Animator>();

        if (!anim) {
            Debug.Log("No animation component in child for the gameobject " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update() {
        //anim.SetFloat("Forward", move.y);
        //anim.SetFloat("Right", move.x);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            health--;
            if (health <= 0) {
                Destroy(gameObject);
            }
            return;
        }
    }
}
