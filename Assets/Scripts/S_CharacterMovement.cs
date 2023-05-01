using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterMovement : MonoBehaviour
{
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
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 CamForwardVec = new Vector3(Horizontal, 0f, Vertical).normalized;


        if (CamForwardVec.magnitude >= 0.1f)
        {
            float NewAngle = Mathf.Atan2(CamForwardVec.x, CamForwardVec.z) * Mathf.Rad2Deg + CameraMain.eulerAngles.y;
            float CurAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, NewAngle, ref SmoothTurnVector, TurnSmoothing);
            transform.rotation = Quaternion.Euler(0f, NewAngle, 0f);

            Vector3 MoveDirVector = Quaternion.Euler(0f, NewAngle, 0f) * Vector3.forward;
            CharacterControl.Move(MoveDirVector.normalized * this.speed * Time.deltaTime);
        }

        CharacterSprintMovement();
        GravityAndJumpCalc();
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
}
