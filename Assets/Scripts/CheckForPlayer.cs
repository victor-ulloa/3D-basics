using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour {
    public Transform playerTransform;

    public float sightDistance = 100f;
    public Transform originPoint;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!playerTransform) {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else {
            Vector3 vector3 = playerTransform.position;
            vector3.z = 0;
            vector3.x = 0;
            transform.LookAt(vector3);
        }
            
        RaycastHit hit;
        if (Physics.Raycast(originPoint.position, Vector3.forward, out hit, sightDistance)) {
            Debug.Log(hit.transform.position.y);
            if (hit.transform.gameObject.tag == "Player") {
                playerTransform = hit.transform;
            }
        } else {
            playerTransform = null;
        }

        Vector3 dir = transform.TransformDirection(Vector3.forward) * sightDistance;
        Debug.DrawRay(originPoint.position, dir, Color.red);
    }
}