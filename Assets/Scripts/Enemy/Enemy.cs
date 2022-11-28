using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;

    [SerializeField] int maxHealth = 10;

    private int _health = 100;
    [SerializeField] public int health {
        get { return _health; }
        set {
            AnimatorStateInfo currentAnimation = anim.GetCurrentAnimatorStateInfo(0);
            if (currentAnimation.IsName("Kicked") ||
                currentAnimation.IsName("Punched")) {
                return;
            }
            
            if (value <= 0) {
                anim.SetTrigger("Death");
                agent.speed = 0;
                Destroy(gameObject, 5.0f);
            } else {
                if (_health - value >= 2) {
                    anim.SetTrigger("Kicked");
                } else {
                    anim.SetTrigger("Punched");
                }
            }

            _health = value;
            if (_health > maxHealth) {
                _health = maxHealth;
            }

            //healthBar.SetHealth(_health);
            Debug.Log("Enemy health is:" + health.ToString());
        }
    }

    Rigidbody rb;

    [SerializeField] enum EnemyState {
        Chase, Patrol
    }

    [SerializeField] EnemyState currentState;
    [SerializeField] public GameObject target;
    [SerializeField] GameObject[] path;
    [SerializeField] int pathIndex;
    [SerializeField] float distThreshold;

    [SerializeField] GameObject[] pickupPrefabs;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();

        rb = GetComponentInChildren<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        if (!anim) {
            Debug.Log("No animation component in child for the gameobject " + gameObject.name);
        }

        if (path.Length <= 0) {
            path = GameObject.FindGameObjectsWithTag("PatrolNode");
        }

        if (currentState == EnemyState.Chase) {
            target = GameObject.FindGameObjectWithTag("Player");

            if (target)
                agent.SetDestination(target.transform.position);
        }

        if (distThreshold <= 0) {
            distThreshold = 0.5f;
        }

    }

    // Update is called once per frame
    void Update() {

        if (currentState == EnemyState.Patrol) {
            if (target)
                Debug.DrawLine(transform.position, target.transform.position, Color.red);

            if (agent.remainingDistance < distThreshold) {
                pathIndex++;
                pathIndex %= path.Length;
                target = path[pathIndex];
            }
        }

        if (currentState == EnemyState.Chase) {
            if (target.CompareTag("PatrolNode"))
                target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target)
            agent.SetDestination(target.transform.position);


        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.Normalize();
        anim.SetFloat("Forward", localVelocity.y);
        anim.SetFloat("Right", localVelocity.x);

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            health--;
        }
    }

    private void OnDestroy() {
        GameObject prefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
        Instantiate(prefab, transform.position, prefab.transform.rotation);
    }

}
