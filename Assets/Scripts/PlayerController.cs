using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float Speed, StrafeSpeed, JumpForce;
    [SerializeField]
    private Rigidbody Hips;
    [SerializeField]
    private bool IsGrounded;

    private PlayerInputActions PlayerInput;

    private InputAction Move;

    Vector2 MoveDir;

    private void Awake()
    {
        PlayerInput = new PlayerInputActions();        
    }
    void Start()
    {
        Hips = GetComponent<Rigidbody>();


    }

    private void OnEnable()
    {
        Move = PlayerInput.Player.Move;
        Move.Enable();
    }
    private void OnDisable()
    {
        Move.Disable();
    }

    private void MovePlayer()
    {
    }

    private void Update()
    {
        MoveDir = Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {

        Hips.AddForce(-Hips.transform.forward * MoveDir.y * Speed, ForceMode.Force);
        Hips.AddForce(Hips.transform.right * MoveDir.x * StrafeSpeed);


        //Vector3 moveDir3 = MoveDir;
        //Hips.MovePosition(Hips.transform.forward + moveDir3 * Time.deltaTime * Speed);

        Debug.Log(MoveDir.y);

    }


}
