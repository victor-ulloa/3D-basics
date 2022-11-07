using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour {
    public Transform playerTransform;

    [SerializeField] float sightDistance = 100f;
    [SerializeField] Transform originPoint;

    [SerializeField] float rotationSpeed;

    public bool isLookingAtPlayer {
        get {
            return !!playerTransform;
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!playerTransform) {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else {
            transform.LookAt(playerTransform.position);
        }

        var ray = new Ray(originPoint.position, this.transform.forward);
            
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightDistance)) {
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