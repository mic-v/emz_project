using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed;
    public float StrafeSpeed;
    public float JumpForce;

    public Rigidbody Hips;
    public bool IsGrounded;

    void Start()
    {
        Hips = GetComponent<Rigidbody>();   
    }
    private void FixedUpdate()
    {
    }

}
