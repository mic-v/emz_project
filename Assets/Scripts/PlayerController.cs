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
    private Transform LeftFoot;

    [SerializeField]
    private Transform RightFoot;

    [SerializeField]
    private Transform RightLeg;

    [SerializeField]
    private Transform LeftLeg;

    private PlayerInputActions PlayerInput;

    private InputAction Move;

    private float CurrentRot = 0;
    private float RotVel = 1f;
    private float RotSmoothTime = 0.5f;
    private float YRotateSensitivity = 100.0f;

    public Vector3 FrontRotation = new Vector3(100,0,0);
    public Vector3 BackRotation = new Vector3(-40, 0, 0);
    public Vector3 LeftOffset = new Vector3(0.2f,0,0);
    public Vector3 RightOffset = new Vector3(-0.2f, 0, 0);
    bool UsingLeft = false;

    public GameObject CubeLeft;
    public GameObject CubeRight;

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

        
        
        
        
        
        
        if(MoveDir.y != 0)
        {

            Vector3 CastRawDirection = MoveDir.y > 0 ?  Vector3.forward : Vector3.back;
            Quaternion NewQuat = new Quaternion(CastRawDirection.x, CastRawDirection.y, CastRawDirection.z, 0) * ( MoveDir.y > 0 ? Quaternion.Euler(FrontRotation) : Quaternion.Euler(BackRotation));

            Vector3 CastFinalDirection = new Vector3(NewQuat.x, NewQuat.y, NewQuat.z);


            int NewLayerMask = 1 << 3;

            NewLayerMask = ~NewLayerMask;


            RaycastHit NewCast;
            Transform NewTransToUse = UsingLeft == true ? LeftLeg.transform : RightLeg.transform;
            if (Physics.Raycast(NewTransToUse.position, NewTransToUse.TransformDirection(CastFinalDirection), out NewCast, Mathf.Infinity, NewLayerMask))
            {
                Debug.DrawRay(NewTransToUse.position, NewTransToUse.TransformDirection(CastFinalDirection) * NewCast.distance, Color.red);
                //Debug.Log("Hit!" + NewCast.collider.name);

                //Debug.Log("x: " + transform.rotation.x + " y: " + transform.rotation.y + " z: " + transform.rotation.z);
                if (UsingLeft == true)
                {
                    Vector3 NewVecAdjusted = new Vector3(LeftOffset.x, LeftOffset.y, LeftOffset.z);// + NewCast.point;
                    Quaternion RotatedOffsetQuat =  new Quaternion(NewVecAdjusted.x, NewVecAdjusted.y, NewVecAdjusted.z, 0);

                    Vector3 RotatedOffsetPos = new Vector3(RotatedOffsetQuat.x, RotatedOffsetQuat.y, RotatedOffsetQuat.z);
                    CubeLeft.transform.position = NewCast.point;// + RotatedOffsetPos;
                    UsingLeft = false;
                }
                else
                {

                    Vector3 NewVecAdjusted = new Vector3(RightOffset.x, RightOffset.y, RightOffset.z);// + NewCast.point;
                    Quaternion RotatedOffsetQuat = new Quaternion(NewVecAdjusted.x, NewVecAdjusted.y, NewVecAdjusted.z, 0) ;

                    Vector3 RotatedOffsetPos = new Vector3(RotatedOffsetQuat.x, RotatedOffsetQuat.y, RotatedOffsetQuat.z);
                    CubeRight.transform.position = NewCast.point;// + RotatedOffsetPos;
                    UsingLeft = true;
                }

            }
            else
            {
                //Debug.Log("Not Hit!");
                Debug.DrawRay(NewTransToUse.position, NewTransToUse.TransformDirection(CastFinalDirection) * 1000, Color.yellow);
            }

        }


        HipsRB.AddForce(HipsRB.transform.forward * MoveDir.y * Speed, ForceMode.Force);
        //HipsRB.AddForce(-HipsRB.transform.right * MoveDir.x * StrafeSpeed, ForceMode.Force);

        float RotateY = MoveDir.x * YRotateSensitivity * Time.fixedDeltaTime;
        CurrentRot = Mathf.SmoothDampAngle(CurrentRot, RotateY, ref RotVel, RotSmoothTime * Time.fixedDeltaTime);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, CurrentRot, 0);



        ProcessJumping();

        //Vector3 moveDir3 = MoveDir;
        //Hips.MovePosition(Hips.transform.forward + moveDir3 * Time.deltaTime * Speed);

    }
}


