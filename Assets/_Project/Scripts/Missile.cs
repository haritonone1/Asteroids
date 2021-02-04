using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    private float _damage = 50;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity += new Vector2( -transform.up.x, -transform.up.y)  * _maxSpeed;
        Invoke(nameof(DestroyYourself),2f);
    }

    public void ObtainDamage(float damage)
    {
        _damage = damage;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var spaceObject = other.collider.gameObject.GetComponent<SpaceObject>();

        if (spaceObject != null)
        {
            spaceObject.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private void DestroyYourself()
    {
        Destroy(gameObject);
    }
}
