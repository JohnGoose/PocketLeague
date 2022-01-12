using UnityEngine;

[RequireComponent(typeof(Controller))]
public class PlayerBoosting : MonoBehaviour
{
    const float BoostForce = 991 / 100;
    
    Controller _c;
    Rigidbody _rb;
    
    private void Start()
    {
        _c = GetComponent<Controller>();
        _rb = GetComponentInParent<Rigidbody>();
        
        // Activate ParticleSystems GameObject
        if (Resources.FindObjectsOfTypeAll<PlayerParticleSystem>()[0] != null)
            Resources.FindObjectsOfTypeAll<PlayerParticleSystem>()[0].gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Boosting();
    }
    
    void Boosting()
    {
        if (GameManager.MobileInput.isBoost && _c.forwardSpeed < Controller.MaxSpeedBoost)
        {
                _rb.AddForce(BoostForce * transform.forward, ForceMode.Acceleration);
        }
    }
}
