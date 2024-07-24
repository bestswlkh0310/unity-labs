using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    // Object
    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    private Player _player;

    // Zombie Move
    private float _moveSpeed = 0.6f;
    
    // Zombie Attack
    private float _attackRange = 2f;
    private float _attackInterval = 1f;
    private float _nextAttackTime = 0;
    
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

    }

    private void Update()
    {
        Move();
        TryAttack();
    }
    
    private void Move()
    {
        var playerPos = _playerTransform.position;
        transform.LookAt(_playerTransform);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, _moveSpeed * Time.deltaTime);
    }

    private void TryAttack()
    {
        float distance = Vector3.Distance(_playerTransform.position, transform.position);
        var currentTime = Time.time;
        if (distance <= _attackRange && _nextAttackTime <= currentTime)
        {
            Attack();
            _nextAttackTime = currentTime + _attackInterval;
        }
    }
    
    private void Attack()
    {
        _player.Hp -= 10;
    }
}
