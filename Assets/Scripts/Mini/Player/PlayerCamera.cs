using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public float sensitivity = 500f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float yRotation = 90f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -60f, 45f);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
