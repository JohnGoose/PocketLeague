using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [Header("Car State")]
    public bool isAllWheelsSurface = false;
    public bool isCanDrive;
    public float forwardSpeed = 0, forwardSpeedSign = 0, forwardSpeedAbs = 0;
    public int numWheelsSurface;
    public bool isBodySurface;
    public CarStates carState;
    
    [Header("Other")]
    public Transform cogLow;
    public GameObject sceneViewFocusObject;
    
    public const float MaxSpeedBoost = 2300 / 100;

    Rigidbody _rb;
    GUIStyle _style;
    PlayerSphereCollider[] _sphereColliders;
    
    public enum CarStates
    {
        AllWheelsGround,
        Air,
        AllWheelsSurface,
        SomeWheelsSurface,
        BodySideGround,
        BodyGroundDead
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = cogLow.localPosition;
        _rb.maxAngularVelocity = 5.5f;

        _sphereColliders = GetComponentsInChildren<PlayerSphereCollider>();
        
        // Lock scene view camera to the car
        Selection.activeGameObject = sceneViewFocusObject;
        SceneView.lastActiveSceneView.FrameSelected(true);
    }
    
    void FixedUpdate()
    {
        SetCarState();
        UpdateCarVariables();
        //TODO:  limit _rb.velocity.magnitude to < maxSpeedBoost
    }

    private void UpdateCarVariables()
    {
        forwardSpeed = Vector3.Dot(_rb.velocity, transform.forward);
        forwardSpeed = (float) System.Math.Round(forwardSpeed, 2);
        forwardSpeedAbs = Mathf.Abs(forwardSpeed);
        forwardSpeedSign = Mathf.Sign(forwardSpeed);
    }
    
    void SetCarState()
    {
        int temp = _sphereColliders.Count(c => c.isTouchingSurface);
        numWheelsSurface = temp;
        
        isAllWheelsSurface = numWheelsSurface >= 3;

        // All wheels are touching the ground
        if (isAllWheelsSurface)
            carState = CarStates.AllWheelsSurface;

        // Some wheels are touching the ground, but not the body
        if (!isAllWheelsSurface && !isBodySurface)
            carState = CarStates.SomeWheelsSurface;

        // We are lying on our side
        if (isBodySurface && !isAllWheelsSurface)
            carState = CarStates.BodySideGround;

        // All wheels on the ground
        if (isAllWheelsSurface && Vector3.Dot(Vector3.up, transform.up) > 0.95f)
            carState = CarStates.AllWheelsGround;

        // He is dead Jimmy!
        if (isBodySurface && Vector3.Dot(Vector3.up, transform.up) < -0.95f)
            carState = CarStates.BodyGroundDead;

        // In the air
        if (!isBodySurface && numWheelsSurface == 0)
            carState = CarStates.Air;

        isCanDrive = carState == CarStates.AllWheelsSurface || carState == CarStates.AllWheelsGround;
    }

    void DownForce()
    {
        if (carState == CarStates.AllWheelsSurface || carState == CarStates.AllWheelsGround)
            _rb.AddForce(-transform.up * 5, ForceMode.Acceleration);
    }
}