using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    [SerializeField] private float acceleration  = 3f;

    private Rigidbody2D rb;
 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        if(Input.GetKey(KeyCode.P))
            rb.velocity += new Vector2(1, 0.0f) * acceleration;
        if(Input.GetKey(KeyCode.O))
            rb.velocity += new Vector2(-1, 0.0f) * acceleration;
    }
}
