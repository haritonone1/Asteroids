using System;
using System.Collections;
using UnityEngine;

public class Player : SpaceObject
{
    [SerializeField] private float _flightSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _missileAppearingPosition;
    [SerializeField] private Missile _missilePrefab;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private Joystick _joystick;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Coroutine _routine;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // var up = _transform.up;
            // _rigidbody2D.velocity += new Vector2(up.x, up.y) * Time.deltaTime * _flightSpeed;
            _transform.position += -Vector3.left * _flightSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _transform.position += Vector3.left * _flightSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(new Vector3(0f,0f,1f * _rotationSpeed));
            _transform.position += Vector3.up * _flightSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.D))
        {
            _transform.position += -Vector3.up * _flightSpeed * Time.deltaTime;
        }

        transform.position += Vector3.right * _joystick.Horizontal * Time.deltaTime * _flightSpeed;
        transform.position += Vector3.up * _joystick.Vertical * Time.deltaTime * _flightSpeed;

        // if (Input.GetMouseButtonDown(0))
        // {
        //     CreateMissile();
        // }
    }

    public void CreateMissile()
    {
        var missile = Instantiate(_missilePrefab, transform);
        missile.transform.position = _missileAppearingPosition.position;
        missile.transform.parent = null;
        missile.ObtainDamage(50f);
        missile.AddVelocity(new Vector2(-missile.transform.up.x, -missile.transform.up.y));
    }

    public void ShootFunc()
    {
        if(_routine == null)
            _routine = StartCoroutine(Shoot());
    }

    public void StopShootFunc()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
    }
    

    public IEnumerator Shoot()
    {
        while (true)
        {
            CreateMissile();
            yield return new WaitForSeconds(_shootingDelay);
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
        Actions.OnFinish();
        Destroy(gameObject);
    }
}
