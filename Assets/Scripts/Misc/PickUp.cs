using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public enum PickupType {
        SpeedUp = 0,
        SuperJump = 1,
        Win = 2
    }

    [SerializeReference] PickupType pickupType;
    [SerializeReference] AudioClip PickUpSound;


    private void OnTriggerEnter(Collider collision) {

        PlayerController currentPlayer = collision.gameObject.GetComponent<PlayerController>();

        if (collision.tag == "Player") {

            AudioSourceManager sfxManager = collision.gameObject.GetComponent<AudioSourceManager>();

            switch (pickupType) {
                case PickupType.SpeedUp:
                    currentPlayer.StartSpeedChange();
                    Debug.Log("SpeedUp picked up");
                    break;
                case PickupType.SuperJump:
                    currentPlayer.StartJumpChange();
                    Debug.Log("SuperJump picked up");
                    break;
                case PickupType.Win:
                    GameManager.Instance.EndGame();
                    Debug.Log("Win picked up");
                    break;

            }

            sfxManager.Play(PickUpSound);
            Destroy(gameObject);
        }
    }
}
