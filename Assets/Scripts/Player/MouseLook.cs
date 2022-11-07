using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour {
    public enum RotationAxis { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxis axes = RotationAxis.MouseXAndY;

    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumX = -360.0f;
    public float maximumX = 360.0f;

    public float minimumY = -60.0f;
    public float maximumY = 60.0f;

    float rotationY = 0.0f;
    // Start is called before the first frame update
    void Start() {
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    public void Look(InputAction.CallbackContext context) {
        Vector2 mouseDelta = context.action.ReadValue<Vector2>();

        if (axes == RotationAxis.MouseXAndY) {
            float rotationX = transform.localEulerAngles.y + mouseDelta.x * sensitivityX;

            rotationY += mouseDelta.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        } else if (axes == RotationAxis.MouseX) {
            transform.Rotate(0, mouseDelta.x * sensitivityX, 0);
        } else {
            rotationY += mouseDelta.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}