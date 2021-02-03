using System;
using UnityEngine;
using Random = System.Random;

public class Asteroid : SpaceObject
{

    [SerializeField] private float _maxSpeed;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(UnityEngine.Random.Range(-_maxSpeed,_maxSpeed),UnityEngine.Random.Range(-_maxSpeed,_maxSpeed));
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }
}
