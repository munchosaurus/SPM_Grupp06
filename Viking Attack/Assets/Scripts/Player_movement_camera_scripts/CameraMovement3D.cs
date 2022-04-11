using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement3D : MonoBehaviour
{

    [SerializeField] private GameObject firstPersonPos;
    [SerializeField] private GameObject thirdPersonPos;
    [SerializeField] float mouseSensitivity = 1;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 cameraPos;


    void Start()
    {
        if(GetComponentInParent<PlayerScript3D>().firstPerson)
        {
            cameraPos = firstPersonPos.transform.localPosition;
        }else
        {
            cameraPos = thirdPersonPos.transform.localPosition;
        }
    }
    void Update()
    {
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX,-89,89);
        transform.rotation = Quaternion.Euler(rotationX,rotationY,0f);
    }
    void LateUpdate()
    {
        Vector3 cameraOffset = transform.rotation * cameraPos;
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
