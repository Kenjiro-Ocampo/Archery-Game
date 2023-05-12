using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class S_CharacterMovement : MonoBehaviour
{
    [SerializeField] public Cinemachine.CinemachineFreeLook CharacterCamera; 
    [SerializeField] public CharacterController CharacterControl;
    [SerializeField] public Transform CameraMain;
    [SerializeField] public float speed = 16f;
    [SerializeField] public Transform GroundCheck;
    [SerializeField] public float GroundDistance = 0.4f;
    [SerializeField] private bool bGrounded;
    [SerializeField] public float Gravity = -9.81f;
    [SerializeField] public float MaxJumpHeight = 3f;
    [SerializeField] public float SpeedMultiplyer = 2.5f;
    [SerializeField] public float MaxSpeed = 10f;
    public LayerMask GroundMask;
    public float TurnSmoothing = 0.1f;
    private float SmoothTurnVector;
    private Vector3 Velocity;
    private float PrivateSpeed;
    [SerializeField] public float MinZoom = 50f;
    [SerializeField] public float MaxZoom = 70f;
    private bool ZoomState = false;
    private float ZoomInterpolate = 0.0f;
    Vector3 AimTarget;
    Vector3 AimDirection;


    void Start()
    {
        // Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PrivateSpeed = this.speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check is grounded
        bGrounded = (Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask));

        if(bGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
        
        // Mouse Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Velocity.y += Gravity * Time.deltaTime;

        CharacterControl.Move(Velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && bGrounded)
        {
            Velocity.y = Mathf.Sqrt(MaxJumpHeight * -2f * Gravity);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraMain.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref SmoothTurnVector, TurnSmoothing);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);



            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            CharacterControl.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        CharacterZoom();
        CharacterSprintMovement();
    }

    private void GravityAndJumpCalc()
    {
        bGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        if (bGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && bGrounded)
        {
            Velocity.y = Mathf.Sqrt(MaxJumpHeight * -2f * Gravity);
        }

        Velocity.y += Gravity * Time.deltaTime;
        CharacterControl.Move(Velocity * Time.deltaTime);
    }

    private void CharacterSprintMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed <= MaxSpeed)
        {
            this.speed = this.speed * SpeedMultiplyer;
        }
        else
        {
            this.speed = PrivateSpeed;
        }
    }

    //Not Working Lerp Fix Later
    private void CharacterZoom()
    {
        if (Input.GetMouseButton(1)) // Aiming
        {
            if (!ZoomState)
            {
                ZoomState = true;
                ZoomInterpolate = 0f;
            }
            CharacterCamera.m_Lens.FieldOfView = Mathf.SmoothStep(70f, 50f, ZoomInterpolate);
            ZoomInterpolate += 4f * Time.deltaTime;
        }
        else
        {
            if (ZoomState) // Not Aiming
            {
                ZoomState = false;
                ZoomInterpolate = 0f;
            }

            CharacterCamera.m_Lens.FieldOfView = Mathf.SmoothStep(50f, 70f, ZoomInterpolate);
            ZoomInterpolate += 3f * Time.deltaTime;
            
        }
    }
}
