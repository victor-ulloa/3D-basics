using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class Turret : MonoBehaviour {

    Enemy enemy;

    [SerializeField] Transform projectileSpawn;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float projectileForce = 10.0f;

    // Start is called before the first frame update

    void Awake() {
        enemy = GetComponent<Enemy>();
    }

    private void Update() {

        if (enemy.currentState == Enemy.EnemyState.Chase) {
            if (!IsInvoking()) {
                InvokeRepeating("Fire", 0, 1.0f);
            }
        } else { 
            if(IsInvoking()) {
                CancelInvoke();
            }
        }

    }

    public void Fire() {
        if (projectilePrefab && projectileSpawn) {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            temp.AddForce(projectileSpawn.forward * projectileForce, ForceMode.Impulse);
            Destroy(temp.gameObject, 1);
        }
    }
}
