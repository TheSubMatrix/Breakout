using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float _initialVelocity = 600f;
    private Rigidbody _rb;
    private bool _isActive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && !_isActive)
        {
            transform.parent = null;
            _isActive = true;
            _rb.isKinematic = false;
            _rb.AddForce(new Vector3(_initialVelocity, _initialVelocity, 0));
        }
    }
}
