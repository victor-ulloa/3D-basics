using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            controller.health = 0;
        }
    }
}
