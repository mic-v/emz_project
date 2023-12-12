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
    private Rigidbody HipsRB;

    [SerializeField]
    private Rigidbody LeftFootRB;

    [SerializeField]
    private Rigidbody RigidFootRB;

    private PlayerInputActions PlayerInput;

    private InputAction Move;
    //private InputAction Jump;

    Vector2 MoveDir;
    float JumpDir;

    private void Awake()
    {
        PlayerInput = new PlayerInputActions();        
    }
    void Start()
    {
        HipsRB = GetComponent<Rigidbody>();


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

    private void Update()
    {
        MoveDir = Move.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) {
        JumpDir = context.ReadValue<float>();
    }


    private bool IsGrounded()
    {
        return GetComponent<Rigidbody>().velocity.y >= 0 && GetComponent<Rigidbody>().velocity.y <= 0.01f;
    }

    private void ProcessJumping()
    {
        if(JumpDir == 1.0f && IsGrounded())
        {
            HipsRB.AddForce(HipsRB.transform.up * JumpDir * JumpForce, ForceMode.Force);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.y);
        HipsRB.AddForce(-HipsRB.transform.forward * MoveDir.y * Speed, ForceMode.Force);
        HipsRB.AddForce(HipsRB.transform.right * MoveDir.x * StrafeSpeed, ForceMode.Force);

        ProcessJumping();

        //Vector3 moveDir3 = MoveDir;
        //Hips.MovePosition(Hips.transform.forward + moveDir3 * Time.deltaTime * Speed);

    }


}
