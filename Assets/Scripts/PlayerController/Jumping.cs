using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Jumping : MonoBehaviour
{
    [Header("Forces")]
    [Range(1,4)]
    public float jumpForceMultiplier = 1;
    public int upForce = 3;
    public int upTorque = 50;

    float _jumpTimer = 0;
    bool _isCanFirstJump = false;
    bool _isJumping = false;
    bool _isCanKeepJumping = false;

    Rigidbody _rb;
    Controller _controller;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _controller = GetComponent<Controller>();
    }

    private void FixedUpdate()
    {
        Jump();
        JumpBackToTheFeet();
    }

	 private void Jump()
    {
        if (_controller.isAllWheelsSurface)
		{
			if (_jumpTimer >= 0.1f)
				_isJumping = false;
			
			_jumpTimer = 0;
			_isCanFirstJump = true;
		}

		else if (!_controller.isAllWheelsSurface)
			_isCanFirstJump = false;

		// DO initial jump once
		if (GameManager.MobileInput.isJumpDown && _isCanFirstJump)
		{
			_isCanKeepJumping = true;
			_isCanFirstJump = false;
			_isJumping = true;
			_rb.AddForce(transform.up * 292 / 100 * jumpForceMultiplier,ForceMode.VelocityChange);

			_jumpTimer += Time.fixedDeltaTime;
		}

		if (GameManager.MobileInput.isJumpUp)
			_isCanKeepJumping = false;

		if (GameManager.MobileInput.isJump && _isJumping && _isCanKeepJumping && _jumpTimer <= 0.2f)
		{
			_rb.AddForce(transform.up * 1458 / 100 * jumpForceMultiplier, ForceMode.Acceleration);
			_jumpTimer += Time.fixedDeltaTime;
		}

		
    }

    //Auto jump and rotate when the car is on the roof
    void JumpBackToTheFeet()
    {
        if (_controller.carState != Controller.CarStates.BodyGroundDead) return;
        
        if (GameManager.MobileInput.isJumpDown) //|| Input.GetButtonDown("A"))
        {
            _rb.AddForce(Vector3.up * upForce, ForceMode.VelocityChange);
            _rb.AddTorque(transform.forward * upTorque, ForceMode.VelocityChange);
        }
    }
}