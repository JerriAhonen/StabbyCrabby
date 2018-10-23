using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTesting : MonoBehaviour {

    private InputReader _inputReader;

    public float velocity = 0.5f;
    public float turnSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float inputPadding = 0.1f;
    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public LayerMask ground;
    public float maxGroundAngle = 120f;
    public bool debugLines;

    Vector3 movementVector;
    float angle;
    float groundAngle;

    Quaternion targetRotation;
    Transform cam;

    Vector3 forward;
    RaycastHit hitInfo;
    bool grounded;

    private void Awake()
    {
        _inputReader = InputReader.Instance;
    }

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        GetInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();

        if (Mathf.Abs(movementVector.x) < inputPadding && Mathf.Abs(movementVector.z) < inputPadding) return;
        
        Move();
        Rotate();
    }

    void GetInput()
    {
        movementVector.x = _inputReader.MovementInputX;
        movementVector.z = _inputReader.MovementInputZ;
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(movementVector.x, movementVector.z);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        //targetRotation = Quaternion.Euler(0, angle, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        Quaternion rot = _inputReader.LocalRotation;
        rot.x = 0;
        rot.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }

    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;
        //transform.position += forward * velocity * Time.deltaTime;

        transform.Translate(movementVector * velocity  * Time.deltaTime);
    }

    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(transform.right, hitInfo.normal);
    }

    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, height + heightPadding, ground))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < height)
            {
                transform.position = Vector3.Lerp(transform.position,
                                                transform.position + Vector3.up * height,
                                                5 * Time.deltaTime);
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    void DrawDebugLines()
    {
        if (!debugLines) return;

        Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (height + heightPadding), Color.green);
    }
}
