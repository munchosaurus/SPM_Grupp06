using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyNameDisplay : NetworkBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Text text;
    [SerializeField] private GameObject nameSource;
    [SerializeField] private uint netIDOfSpottedPlayer;
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

    public uint GetPersonalInstanceID()
    {
        return netIDOfSpottedPlayer;
    }
    
    // public void Setup(Transform parent, Transform player, GlobalPlayerInfo playerInfo, Camera mainCam)
    // {
    //     Debug.Log(gameObject.GetInstanceID());
    //     transform.SetParent(parent.Find("UI"));
    //     nameSource = player.gameObject;
    //     target = player.Find("Overhead").gameObject.transform;
    //     instanceID = player.gameObject.GetInstanceID();
    //     text.text = playerInfo.GetName();
    //     mainCamera = mainCam;
    // }

    public void Setup(Transform parent, uint spottedPlayerNetID, Camera mainCam)
    {
        transform.SetParent(parent.Find("UI"));
        nameSource = NetworkServer.spawned[spottedPlayerNetID].gameObject;
        target = nameSource.transform;
        netIDOfSpottedPlayer = spottedPlayerNetID;
        text.text = nameSource.GetComponent<GlobalPlayerInfo>().GetName();
        mainCamera = mainCam;
    }
}