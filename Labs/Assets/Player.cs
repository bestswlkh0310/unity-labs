using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMove : MonoBehaviour
{
    // MARK: - Properties
    
    // Object
    private Transform _cameraTransform;
    private CharacterController _characterController;
    private LayerMask _fieldLayer;

    // Player Move
    private float _moveSpeed = 5f;
    private float _jumpSpeed = 5f;
    private float _yVelocity = 0;

    // Player Rotation
    private float _rotationX;
    private float _rotationY;
    
    // Camera
    private const float Sensitivity = 500f;
    
    // MARK: - Method
    private void Start()
    {
        _cameraTransform = Camera.main?.GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        _fieldLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var moveDirection = new Vector3(h, 0, v);
        moveDirection = _cameraTransform.TransformDirection(moveDirection); // 원래 World 좌표 기준인데 카메라 좌표 기준으로 변환
        moveDirection *= _moveSpeed;
        if (IsGrounded())
        {
            _yVelocity = 0;
            if (Input.GetKey(KeyCode.Space)) // Jump
            {
                _yVelocity = _jumpSpeed;
            }
        }
        
        _yVelocity += Physics.gravity.y * Time.deltaTime;
        moveDirection.y = _yVelocity;
        _characterController.Move(moveDirection * Time.deltaTime);
        _cameraTransform.transform.position = transform.position;
    }

    private void Rotate()
    {
        var mouseMoveX = Input.GetAxis("Mouse X");
        var mouseMoveY = Input.GetAxis("Mouse Y");

        _rotationY += mouseMoveX * Sensitivity * Time.deltaTime;
        _rotationX += mouseMoveY * Sensitivity * Time.deltaTime;
        
        _rotationX = Mathf.Clamp(_rotationX, -90, 90f);

        transform.eulerAngles = new Vector3(0, _rotationY, 0);
        _cameraTransform.eulerAngles = new Vector3(-_rotationX, _rotationY, 0);
    }

    private bool IsGrounded()
    {
        if (_characterController.isGrounded) return true;
        
        var maxDistance = 1.1f; // 탐색 거리
        
        // 발사하는 광선의 초기 위치와 방향
        // 약간 신체에 박혀 있는 위치로부터 발사하지 않으면 제대로 판정할 수 없을 때가 있다.
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        // Raycast의 hit 여부로 판정
        // 지상에만 충돌로 레이어를 지정
        return Physics.Raycast(ray, maxDistance, _fieldLayer);
    }
}