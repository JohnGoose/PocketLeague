using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public float throttleInput, steerInput, yawInput, pitchInput, rollInput;
    public bool isBoost, isDrift, isAirRoll, isBallcam;
    public bool isJump, isJumpUp, isJumpDown;

	public Joystick joystick;
	public Jumpbutton jumpbutton;
	public Boostbutton boostbutton;
	public Driftbutton driftbutton;
	public Ballcambutton ballcambutton;

    void Update()
    {
        throttleInput = GetThrottle();
        steerInput = GetSteerInput();

        yawInput = joystick.Horizontal;    //Input.GetAxis("Horizontal");
        pitchInput = joystick.Vertical;
        rollInput = GetRollInput();
		isJump = false; //joybutton.Pressed; //|| Input.GetButton("A");
        isJumpUp = jumpbutton.NotPressed; //|| Input.GetButtonUp("A");
        isJumpDown = jumpbutton.Pressed;
        
        isBoost = boostbutton.Pressed; // Input.GetButton("RB") || 
        isDrift =  driftbutton.Pressed; // Input.GetButton("LB") ||
        isAirRoll =  Input.GetKey(KeyCode.LeftShift); //Input.GetButton("LB") ||
		isBallcam = ballcambutton.Pressed;
    }

    private float GetRollInput()
    {
        var inputRoll = 0;
        if (boostbutton.Pressed) // || Input.GetButton("B"))
            inputRoll = -1;

        return inputRoll;
    }

	float GetThrottle()
    {
        float throttle = 1;
        // if (joystick.Vertical  > 0) // || Input.GetAxis("RT") > 0
        //     throttle = Mathf.Max(joystick.Vertical); // , Input.GetAxis("RT"));
        if (joystick.Vertical <= -0.4f && joystick.Vertical >= -0.6f) //|| Input.GetAxis("LT") < 0)
            throttle = 0; //, Input.GetAxis("LT"));
		else if (joystick.Vertical <= -0.6f) //|| Input.GetAxis("LT") < 0)
            throttle = Mathf.Min(joystick.Vertical);
		else
			throttle = 1;

        return throttle;
    }

    float GetSteerInput()
    {
        //return Mathf.MoveTowards(steerInput, Input.GetAxis("Horizontal"), Time.fixedDeltaTime);
        return joystick.Horizontal;
    }
}
