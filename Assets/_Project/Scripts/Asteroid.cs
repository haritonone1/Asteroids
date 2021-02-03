using System;
using Unity.Mathematics;
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

    private void CreateDuplicates()
    {
        if (transform.localScale.x < .5f)
            return;
        
        var numberOfDuplicates = UnityEngine.Random.Range(2, 4);
        var duplicatesSize = transform.localScale.x / numberOfDuplicates;
        var duplicateSizeVector = new Vector2(duplicatesSize, duplicatesSize);
        
        for (int i = 0; i < numberOfDuplicates; i++)
        {
            var randomSizeMultiplier = UnityEngine.Random.Range(0.7f, 1.5f);
            var obj = Instantiate(gameObject,transform.position + new Vector3(UnityEngine.Random.Range(-0.5f,0.5f),UnityEngine.Random.Range(-0.5f,0.5f),0f),Quaternion.identity);
            obj.transform.localScale = duplicateSizeVector * randomSizeMultiplier;
        }
    }
    
    protected override void OnDie()
    {
        CreateDuplicates();
        Destroy(gameObject);
    }
}
