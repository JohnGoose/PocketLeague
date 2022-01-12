using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Wheel : MonoBehaviour
{
  //  public float steerAngle;
  //  public float Fx;
    float Fy;
    
    public bool wheelFL, wheelFR, wheelRL, wheelRR;
    
    public Transform wheelMesh;
    private float _meshRevolutionAngle;
    
    Rigidbody _rb;
    Controller _c;
    GroundControl _groundControl;
    
    float _wheelRadius, _wheelForwardVelocity, _wheelLateralVelocity;
    Vector3 _wheelVelocity, _lastWheelVelocity, _wheelAcceleration, _wheelContactPoint, _lateralForcePosition = Vector3.zero;
    
    const float AutoBrakeAcceleration = 5.25f;
    
    //[HideInInspector]
    public bool isDrawWheelVelocities, isDrawWheelDisc, isDrawForces;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _c = GetComponentInParent<Controller>();
        _groundControl= GetComponentInParent<GroundControl>();
        _wheelRadius = transform.localScale.z / 2;
    }
    
    public void RotateWheels(float steerAngle)
    {
        if(wheelFL || wheelFR)
            transform.localRotation = Quaternion.Euler(Vector3.up * steerAngle);
        
        // Update mesh rotations of the wheel
        if (wheelMesh)
        {
            //wheelMesh.transform.position = transform.position;
            wheelMesh.transform.localRotation = transform.localRotation;
            _meshRevolutionAngle += (Time.deltaTime * transform.InverseTransformDirection(_wheelVelocity).z) /
                (2 * Mathf.PI * _wheelRadius) * 360;
            wheelMesh.transform.Rotate(Vector3.right, _meshRevolutionAngle * 1.3f);
            //transform.Rotate(new Vector3(0, 1, 0), steerAngle - transform.localEulerAngles.y);
        }
    }

    private void FixedUpdate()
    {
        UpdateWheelState();

        if (!_c.isCanDrive) return;
        //ApplyForwardForce();
        ApplyLateralForce();
        SimulateDrag();
    }
    
    public void ApplyForwardForce(float force)
    {
        _rb.AddForce(force * transform.forward, ForceMode.Acceleration);
        
        // Kill velocity to 0 for small car velocities
        if (force == 0 && _c.forwardSpeedAbs < 0.1)
            _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, 0);
    }
    
    private void ApplyLateralForce()
    {
        Fy = _wheelLateralVelocity * _groundControl.currentWheelSideFriction ;
        _lateralForcePosition = transform.localPosition;
        _lateralForcePosition.y = _c.cogLow.localPosition.y;
        _lateralForcePosition = _c.transform.TransformPoint(_lateralForcePosition);
        _rb.AddForceAtPosition(-Fy * transform.right, _lateralForcePosition, ForceMode.Acceleration);
    }
    
    private void SimulateDrag()
    {
        //Applies auto braking if no input, simulates air and ground drag
        if (!(_c.forwardSpeedAbs >= 0.1)) return;
        
        //TODO Make a separate function
        var dragForce = AutoBrakeAcceleration / 4 * _c.forwardSpeedSign * (1 - Mathf.Abs(GameManager.MobileInput.throttleInput));
        _rb.AddForce(-dragForce * transform.forward, ForceMode.Acceleration);
    }

    private void UpdateWheelState()
    {
        _wheelContactPoint = transform.position - transform.up * _wheelRadius;
        _wheelVelocity = _rb.GetPointVelocity(_wheelContactPoint);
        _wheelForwardVelocity = Vector3.Dot(_wheelVelocity, transform.forward);
        _wheelLateralVelocity = Vector3.Dot(_wheelVelocity, transform.right);

        _wheelAcceleration = (_wheelVelocity - _lastWheelVelocity) * Time.fixedTime;
        _lastWheelVelocity = _wheelVelocity;
    }
}