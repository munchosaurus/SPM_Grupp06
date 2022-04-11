using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHelpFunctions3D : MonoBehaviour
{
    
    public Vector3 CalculateNormalForce(Vector3 velocity, Vector3 normal)
    {
        Vector3 projection;
        if(Vector3.Dot(velocity, normal) > 0)
            projection = Vector3.zero;
        else
            projection = Vector3.Dot(velocity, normal) * normal;
        return -projection;
    }

}
