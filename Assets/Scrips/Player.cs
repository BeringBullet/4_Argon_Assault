using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 5f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 3f;
    [SerializeField] float PositionPitchFactor = -5f;
    [SerializeField] float ControlPitchFactor = -20f;
    [SerializeField] float PositionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    
    private void ProcessMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float xRaw = transform.localPosition.x + xOffset;
        float yRaw = transform.localPosition.y + yOffset;

        float xClamped = Mathf.Clamp(xRaw, -xRange, xRange);
        float yClamped = Mathf.Clamp(yRaw, -yRange, yRange);

        transform.localPosition = new Vector3(xClamped, yClamped, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPositon = transform.localPosition.y * PositionPitchFactor;
        float pitchControlDueToPosition = yThrow * ControlPitchFactor;
        float pitch = pitchDueToPositon + pitchControlDueToPosition;

        float yawDueToPositon = transform.localPosition.x * PositionYawFactor;
        float yaw = yawDueToPositon;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
