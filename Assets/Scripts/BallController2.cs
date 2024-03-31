using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : MonoBehaviour
{
    public float _initialVelocity = 600f;
    private Rigidbody _rb;
    private bool _isActive;


    [SerializeField] LineRenderer _lineRenderer;
    Vector3 mousePosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
            if(_lineRenderer != null)
            {
                _lineRenderer.enabled = true;
            }
        
    }

    private void Update()
    {
        
        if (!_isActive)
        {
            Vector3 position = gameObject.transform.InverseTransformDirection(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            mousePosition = Camera.main.ScreenToWorldPoint(position);
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + ((mousePosition - transform.position).normalized *6));
        }
        if (Input.GetButtonDown("Fire1") && !_isActive)
        {
            transform.parent = null;
            _isActive = true;
            _rb.isKinematic = false;
            _rb.AddForce(new Vector3(_initialVelocity, _initialVelocity, 0));
        }
    }
}
