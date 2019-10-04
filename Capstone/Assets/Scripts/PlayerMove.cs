﻿using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Camera playerCamera = null;
    public float limitY = 60.0f;
    public float cameraSmooth = 10.0f;
    public float speed = 10.0f;

    private float translation;
    private float straffe;
    private float lookVertical = 0.0f;

    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ToggleInteract();
        }

    }

    public void ToggleInteract() 
    {

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
                //Debug.Log("Intracted with " + interactable.name);
            }
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.MovePosition(rb.transform.position += (rb.transform.forward * Input.GetAxis("Vertical") + rb.transform.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime);

        lookVertical += Input.GetAxisRaw("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        rb.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraSmooth * Time.deltaTime);
    }
}