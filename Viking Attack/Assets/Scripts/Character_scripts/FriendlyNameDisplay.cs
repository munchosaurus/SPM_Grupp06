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

    public void Update()
    {
        Display();
    }
    
    public void Display()
    {
        var wantedPos = Camera.main.WorldToScreenPoint (target.position);
        gameObject.transform.position = wantedPos;
    }

    public int GetPersonalInstanceID()
    {
        return instanceID;
    }
    
    public void Setup(Transform parent, Transform player, GlobalPlayerInfo playerInfo)
    {
        transform.SetParent(parent.Find("UI"));
        nameSource = player.gameObject;
        target = player.Find("Overhead").gameObject.transform;
        instanceID = player.gameObject.GetInstanceID();
        text.text = playerInfo.GetName();
    }
}
