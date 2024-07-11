using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float yMin = 2;
    private float yMax = 7;

    private void Update() {
        HandleMovement();       
        HandleRotation();
        HandleYMovement();
    }

    private void HandleYMovement() {
        Vector3 followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        
        followOffset.y += Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
        followOffset.y = Mathf.Clamp(followOffset.y, yMin, yMax);
        
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = followOffset;
    }

    private void HandleRotation() {
        float rotateDir = 0;

        if (Input.GetMouseButton(1)) // Right mouse button is pressed
        {
            float mouseX = Input.GetAxis("Mouse X");
            if (mouseX > 0) rotateDir = 1f;
            if (mouseX < 0) rotateDir = -1f;
        }

        transform.Rotate(Vector3.up, rotationSpeed * rotateDir * Time.deltaTime);
    }

    private void HandleMovement(){
        Vector3 inputDir = new(0, 0, 0);

        if(Input.GetKey(KeyCode.W)) inputDir.z = 1f;
        if(Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if(Input.GetKey(KeyCode.D)) inputDir.x = 1f;
        if(Input.GetKey(KeyCode.A)) inputDir.x = -1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += Time.deltaTime * moveSpeed * moveDir; 
    }
}
