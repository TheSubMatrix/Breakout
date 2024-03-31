using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController2 : MonoBehaviour
{
    public float _paddleSpeed = 1f;
    [SerializeField] float offsetLimit = 24.5f;
    private Vector3 _playerPosition;
    private Vector3 _playerStartPosition;
    private void Start()
    {
        _playerStartPosition = transform.position;
        _playerPosition = _playerStartPosition;
    }
    private void Update()
    {
        float newXPosition = transform.position.x + (Input.GetAxis("Horizontal") * _paddleSpeed * Time.deltaTime);
        _playerPosition = new Vector3(Mathf.Clamp(newXPosition, _playerStartPosition.x - offsetLimit, _playerStartPosition.x + offsetLimit), _playerPosition.y, _playerPosition.z);
        transform.position = _playerPosition;
    }
}
