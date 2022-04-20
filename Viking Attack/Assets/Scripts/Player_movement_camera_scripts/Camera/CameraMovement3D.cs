using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CameraMovement3D : NetworkBehaviour
{
    //private GameObject firstPersonPosition;
    //private GameObject thirdPersonPosition;
    [SerializeField] float mouseSensitivity = 1;
    [SerializeField] GameObject player;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 cameraPosition;
    private bool notMoved = true;
    private Camera mainCamera;

    private GameObject whereToPut;
    private Transform cameraTransform;
    private bool oneTime = true;

    [Client]
    void Awake()
    {
        mainCamera = Camera.main;
    }

    [Client]
    void OnStartLocalPlayer()
    {

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

    [Client]
    void Update()
    {
        if (!isLocalPlayer) return;
        mainCamera.gameObject.name += player.name;
        //Sets rotation to camera depending on mouse position and movement
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -89, 89);
        cameraTransform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    [Client]
    void LateUpdate()
    {
        if (!isLocalPlayer) return;

        //Looks if camera hits a collider and if it's true change position of camera to hit position
        Vector3 cameraOffset = transform.rotation * cameraPosition;
        RaycastHit hit;
        if (Physics.SphereCast(whereToPut.transform.position, 0.1f, cameraOffset, out hit, cameraOffset.magnitude, ~(1 << whereToPut.gameObject.layer)))
        {
            mainCamera.transform.position = transform.transform.position + cameraOffset.normalized * hit.distance;
        }
        else
        {
            mainCamera.transform.position = whereToPut.transform.position + cameraOffset;
        }
    }

    public override void OnStopClient()
    {
        if (isLocalPlayer && mainCamera != null)
        {
            mainCamera.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(mainCamera.gameObject, SceneManager.GetActiveScene());
            mainCamera.orthographic = true;
            mainCamera.transform.localPosition = new Vector3(0f, 70f, 0f);
            mainCamera.gameObject.name += "MainCamera";

        }
    }
}
