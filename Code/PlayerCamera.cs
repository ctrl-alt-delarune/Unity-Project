using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private Transform body_transform;

    public Vector2 camera_position = new Vector2(0, 0);
    public float MouseSens = 100.0f;

    void Start()
    {

        body_transform = transform.parent.parent;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        camera_position.x += Input.GetAxis("MouseX") * Time.deltaTime * MouseSens;
        camera_position.y -= Input.GetAxis("MouseY") * Time.deltaTime * MouseSens;

        camera_position.y = Mathf.Clamp(camera_position.y, -90, 90);

        //Modulo to preserve accuracy
        camera_position.x %= 360;

        transform.localRotation = Quaternion.Euler(camera_position.y, 0, 0);
        body_transform.localRotation = Quaternion.Euler(0, camera_position.x, 0);

    }

}
