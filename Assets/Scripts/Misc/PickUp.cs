using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public enum PickupType {
        item1 = 0
    }

    [SerializeReference] PickupType pickupType;
    [SerializeReference] AudioClip PickUpSound;


    private void OnTriggerEnter(Collider collision) {

        PlayerController currentPlayer = collision.gameObject.GetComponent<PlayerController>();

        if (collision.tag == "Player") {

            //AudioSourceManager sfxManager = collision.gameObject.GetComponent<AudioSourceManager>();

            switch (pickupType) {
                case PickupType.item1:
                    Debug.Log("Item 1 picked up");
                    break;
            }

            //sfxManager.Play(PickUpSound);
            Destroy(gameObject);
        }
    }
}
