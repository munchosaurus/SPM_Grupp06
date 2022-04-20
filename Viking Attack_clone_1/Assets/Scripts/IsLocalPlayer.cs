using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IsLocalPlayer : NetworkBehaviour
{
    public bool LocalCheck()
    {
        return isLocalPlayer;
    }
}
