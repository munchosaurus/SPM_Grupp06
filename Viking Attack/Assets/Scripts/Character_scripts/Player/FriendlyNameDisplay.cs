using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyNameDisplay : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Text text;
    [SerializeField] private GameObject nameSource;
    [SerializeField] private int instanceID;
    [SerializeField] private Camera mainCamera;

    public void Update()
    {
        Display();
    }
    
    public void Display()
    {
        if (mainCamera == null)
            return;
        
        var wantedPos = mainCamera.WorldToScreenPoint (target.position);
        gameObject.transform.position = wantedPos;

    }

    public int GetPersonalInstanceID()
    {
        return instanceID;
    }
    
    public void Setup(Transform parentTransform, Transform spottedPlayer, Camera mainCam)
    {
        transform.SetParent(parentTransform.Find("UI"));
        nameSource = spottedPlayer.gameObject;
        target = spottedPlayer.Find("Overhead").gameObject.transform;
        instanceID = spottedPlayer.gameObject.GetInstanceID();
        text.text = spottedPlayer.gameObject.GetComponent<GlobalPlayerInfo>().GetName();
        mainCamera = mainCam;
    }
}
