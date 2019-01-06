using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [Header("General 1")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 5f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 3f;

    [Header("Screen-position Based")]
    [SerializeField] float PositionPitchFactor = -5f;
    [SerializeField] float PositionYawFactor = 5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float ControlPitchFactor = -20f;

    [SerializeField] GameObject[] Guns;

    float xThrow, yThrow = 0f;
    bool isControlEnabled = true;
    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessMovement();
            ProcessRotation();
            ProcessFiring();
        }
    }

 

    /// <summary>
    /// Called by string Method (Danger)
    /// </summary>
    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }


    private void ProcessMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

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

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            foreach (GameObject gun in Guns)
            {
                gun.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject gun in Guns)
            {
                gun.SetActive(false);
            }
        }
    }
}
