using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Camera = UnityEngine.Camera;

public class CameraMovement3D : NetworkBehaviour
{
    [SerializeField]private GameObject firstPersonPosition;
    [SerializeField]private GameObject thirdPersonPosition;
    [SerializeField] float mouseSensitivity = 1;
    [SerializeField] GameObject player;
    private float rotationX;
    private float rotationY;
    private Vector3 cameraPosition;
    private Camera mainCamera;

    private GameObject whereToPut;
    private Transform cameraTransform;
    
    
    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("CameraMain").GetComponent<Camera>();
    }

    public override void OnStartLocalPlayer()
    {
        if (mainCamera != null)
        {
            if (GetComponentInParent<PlayerScript3D>().firstPerson)
            {
                whereToPut = firstPersonPosition;
                cameraPosition = whereToPut.transform.localPosition;
            }
            else
            {
                whereToPut = thirdPersonPosition;
                cameraPosition = whereToPut.transform.localPosition;

            }
            // configure and make camera a child of player with 3rd person offset
            mainCamera.orthographic = false;
            mainCamera.transform.SetParent(transform);
            mainCamera.transform.localPosition = cameraPosition;
            mainCamera.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
            cameraTransform = mainCamera.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab

        }
    }

    public override void OnStopLocalPlayer()
    {
        if (mainCamera != null)
        {
            
            mainCamera.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(mainCamera.gameObject, SceneManager.GetActiveScene());
            mainCamera.orthographic = true;
            mainCamera.transform.localPosition = new Vector3(0f, 70f, 0f);
            mainCamera.transform.localEulerAngles = new Vector3(90f, 0f, 0f);

        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        //Sets rotation to camera depending on mouse position and movement
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -89, 89);
        cameraTransform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        
    }

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
}
