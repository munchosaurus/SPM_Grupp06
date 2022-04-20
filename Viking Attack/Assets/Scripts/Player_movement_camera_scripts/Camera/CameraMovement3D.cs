using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;

public class CameraMovement3D : MonoBehaviour
{
    //private GameObject firstPersonPosition;
    //private GameObject thirdPersonPosition;
    [SerializeField] float mouseSensitivity = 1;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 cameraPosition;
    private bool notMoved = true;
    private Camera mainCamera;

    private GameObject whereToPut;
    private Transform cameraTransform;

    void Start()
    {
        mainCamera = Camera.main;

            if (GetComponentInParent<PlayerScript3D>().firstPerson)
            {
                whereToPut = GameObject.FindWithTag("FirstPerson");
                cameraPosition = whereToPut.transform.localPosition;
            }
            else
            {
                whereToPut = GameObject.FindWithTag("ThirdPerson");
                cameraPosition = whereToPut.transform.localPosition;

            }
            cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            cameraTransform.parent = whereToPut.transform;  //Make the camera a child of the mount point
            cameraTransform.position = whereToPut.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = whereToPut.transform.rotation;
        
        /*mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.transform.parent = GameObject.FindWithTag("Player").transform;*/
        //Sets camera position to firstperson if variable is true in PlayerScript3D

    }
    void Update()
    {

        //Sets rotation to camera depending on mouse position and movement
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX,-89,89);
        transform.rotation = Quaternion.Euler(rotationX,rotationY,0f);
    }
    void LateUpdate()
    {
        //Looks if camera hits a collider and if it's true change position of camera to hit position
        Vector3 cameraOffset = transform.rotation * cameraPosition;
        RaycastHit hit;
        if(Physics.SphereCast(whereToPut.transform.position, 0.1f, cameraOffset, out hit ,cameraOffset.magnitude,~(1 << whereToPut.gameObject.layer)))
        {
            mainCamera.transform.position = transform.transform.position + cameraOffset.normalized * hit.distance;
        }else
        {
            mainCamera.transform.position = whereToPut.transform.position + cameraOffset;
        }
    }
}