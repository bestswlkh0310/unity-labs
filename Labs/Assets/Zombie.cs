using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    // Object
    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    
    // Zombie Move
    private float _moveSpeed = 1;
    
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        var playerPos = _playerTransform.position;
        transform.LookAt(_playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, _moveSpeed * Time.deltaTime);
    }
}
