using UnityEditor;
using UnityEngine;

public class PlayerSphereCollider : MonoBehaviour
{
    public bool isTouchingSurface = false;
    
    //Raycast options
    float _rayLen, _rayOffset = 0.05f;
    Vector3 _rayContactPoint, _rayContactNormal;
    
    Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _rayLen = transform.localScale.x / 2 + _rayOffset;
    }
    
    private void FixedUpdate()
    {
        isTouchingSurface = IsRayContact() || _isColliderContact;
        
        //TODO: this class should only do raycasts and sphere collider ground detection. Move to CubeWheel or CubeController
        if (isTouchingSurface)
            ApplyStickyForces(StickyForceConstant*5, _rayContactPoint, -_rayContactNormal);
    }

    const int StickyForceConstant = 325 / 100;
    private void ApplyStickyForces(float stickyForce, Vector3 position, Vector3 dir)
    {
        var force = stickyForce / 4 * dir;
        
        //_rb.AddForceAtPosition(stickyForce, _contactPoint, ForceMode.Acceleration);
        _rb.AddForceAtPosition(force, position, ForceMode.Acceleration);
        //Debug.DrawRay(position, force, Color.blue, 0, true);
    }

    // Does a wheel touches the ground? Using raycasts, not sphere collider contact point, since no suspension
    bool IsRayContact()
    {
        var isHit = Physics.Raycast(transform.position, -transform.up, out var hit, _rayLen);
        _rayContactPoint = hit.point;
        _rayContactNormal = hit.normal;
        return false || isHit;
    }

    bool _isColliderContact;
}
