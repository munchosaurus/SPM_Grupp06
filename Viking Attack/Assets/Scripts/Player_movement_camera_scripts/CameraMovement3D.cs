using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement3D : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonPosition;
    [SerializeField] private GameObject thirdPersonPosition;
    [SerializeField] float mouseSensitivity = 1;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 cameraPosition;


    void Start()
    {
        //Sets camera position to firstperson if variable is true in PlayerScript3D
        if(GetComponentInParent<PlayerScript3D>().firstPerson)
        {
            cameraPosition = firstPersonPosition.transform.localPosition;
        }else
        {
            cameraPosition = thirdPersonPosition.transform.localPosition;
        }
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
        if(Physics.SphereCast(transform.parent.transform.position, 0.1f, cameraOffset, out hit ,cameraOffset.magnitude,~(1 << transform.parent.gameObject.layer)))
        {
            transform.position = transform.parent.transform.position + cameraOffset.normalized * hit.distance;
        }else
        {
            transform.position = transform.parent.transform.position + cameraOffset;
        }
    }
}
