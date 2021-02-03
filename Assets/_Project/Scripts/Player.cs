﻿using System;
using UnityEngine;

public class Player : SpaceObject
{
    [SerializeField] private float _flightSpeed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    
    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var up = _transform.up;
            _rigidbody2D.velocity += new Vector2(-up.x, -up.y) * Time.deltaTime * _flightSpeed;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f,0f,1f * _rotationSpeed));
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f,0f,-1f * _rotationSpeed));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var spaceObject = other.gameObject.GetComponent<SpaceObject>();
        if (spaceObject != null)
        {
            OnDie();
        }
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }
}