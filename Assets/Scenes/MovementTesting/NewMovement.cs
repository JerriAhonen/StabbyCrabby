using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour {

    private InputReader _inputReader;

    // MOVEMENT //

    public float velocity;
    public float movementSpeedMult;
    public float rotationSpeed = 10.0f;
    private Vector3 movementVector;
    private Vector3 forward;
    private bool grounded;
    public float inputPadding = 0.1f;
    
    // ROTATION //
    
    private float angle;
    private float groundAngle;

    // GROUND CHECKS //

    public LayerMask ground;
    public float maxGroundAngle = 120f;

    private RaycastHit hitInfo;

    public float height = 0.5f;
    public float heightPadding = 0.05f;

    // DEBUGGING //

    public bool debugLines;

    private void Awake()
    {
        _inputReader = InputReader.Instance;
    }

    /// <summary>
    /// Only do Move() and Rotate if we have some Movement Input.
    /// </summary>
    private void Update()
    {
        GetInput();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();

        if (Mathf.Abs(movementVector.x) < inputPadding && Mathf.Abs(movementVector.z) < inputPadding) return;

        Move();
        Rotate();
    }

    /// <summary>
    /// Gets the input form InputReader, which is a Component of the Player.
    /// </summary>
    void GetInput()
    {
        movementVector.x = _inputReader.MovementInputX;
        movementVector.z = _inputReader.MovementInputZ;
    }

    /// <summary>
    /// 1. Check if ground angle isn't too steep.
    /// 2. Clamp the lenght of the movement vector to maximum 1 so
    ///     we don't move faster towards corners.
    /// 3. Add a little speed multiplier to sideways movement since we are a Crab.
    /// </summary>
    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;

        // CLAMPING TO 1

        if (movementVector != Vector3.zero)
            movementVector = Vector3.ClampMagnitude(movementVector, 1);
        

        // SIDEWAYS MOVEMENT MULTIPLIER

        if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.z))
            movementSpeedMult = 3f;
        else
            movementSpeedMult = 2f;

        transform.Translate(movementVector * velocity * movementSpeedMult * Time.deltaTime);
        // TODO: This may require to take in consideration the Vector3 "forward"!!
    }

    /// <summary>
    /// 1. Get the rotation Quaternion from InputReader's CameraInput.
    /// 2. Set the X and Z axis to 0 so we only follow the camera on the Y axis.
    /// 3. Lerp the rotation for smoother movement.
    /// </summary>
    void Rotate()
    {
        Quaternion rot = _inputReader.LocalRotation;
        rot.x = 0;
        rot.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 1. If we are not grounded, set the ground angle to 90. MAY WANT TO CHANGE THIS
    /// 2. GroundAngle is the angle between the the player's forward vector and the floor's normal.
    /// </summary>
    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    /// <summary>
    /// 1. Raycast straight down from the middle of the Character for "height" + "heightPadding" distance.
    /// 1.1. NOTICE that height needs to be half of the character's colliders height!
    /// 2. If we are grounded, correct our position with lerp if we sink in the ground.
    /// </summary>
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

    /// <summary>
    /// Simple Gravity using Unity's built in Physics.
    /// </summary>
    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    /// <summary>
    /// Debug lines for visualisation of our vectors.
    /// </summary>
    void DrawDebugLines()
    {
        if (!debugLines) return;

        Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (height + heightPadding), Color.green);
    }
}
