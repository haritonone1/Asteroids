using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Alien : SpaceObject
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _shootRange;
    [Header("Missile")]
    [SerializeField] private Missile _missile;
    [SerializeField] private Transform _missileAppearingPosition;
    [SerializeField] private float _missileDamage;
    private Transform _playerTransform;
    private Coroutine _shootRoutine;
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        Actions.OnFinish += StopShootRoutine;
        _playerTransform = GameController.instance.ReturnPlayer();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(UnityEngine.Random.Range(-_maxSpeed,_maxSpeed),UnityEngine.Random.Range(-_maxSpeed,_maxSpeed));
        _shootRoutine = StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (_playerTransform == null)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (Vector2.Distance(_playerTransform.position, transform.position) < _shootRange)
            {
                CreateMissile();
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    private void StopShootRoutine()
    {
        StopCoroutine(_shootRoutine);
    }
    
    private void CreateMissile()
    {
        var missile = Instantiate(_missile, transform);
        missile.transform.position = _missileAppearingPosition.position;
        missile.transform.parent = null;
        missile.ObtainDamage(_missileDamage);
        missile.MakeDangerousForPlayer();
        var direciton = GetDirectionToPlayer(new Vector2(missile.transform.position.x, missile.transform.position.y));
        var random = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        missile.AddVelocity(direciton + random);
    }

    private Vector2 GetDirectionToPlayer(Vector2 missilePosition)
    {
        var direction = (missilePosition - new Vector2(_playerTransform.position.x, _playerTransform.position.y)).normalized;
        return -direction;
    }

    private void OnDestroy()
    {
        Actions.OnFinish -= StopShootRoutine;
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }
}
