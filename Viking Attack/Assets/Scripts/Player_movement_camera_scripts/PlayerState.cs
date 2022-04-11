using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    private PlayerScript3D p;
    public PlayerScript3D player => p = p ?? (PlayerScript3D)owner;
}
