using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateByWaypoints : MonoBehaviour
{

    [SerializeField] private List<Alien> _aliens;
    [SerializeField] private List<Waypoints> _waypoints = new List<Waypoints>();
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(StartSpawnEnemies());
    }

    private IEnumerator StartSpawnEnemies()
    {
        while (true)
        {
            var numberOfEnemies = Random.Range(0, 5);
            var alien = _aliens[Random.Range(0, _aliens.Count)];
            for (int i = 0; i < numberOfEnemies; i++)
            {
                var newAlien = SpawnEnemy(alien);
                newAlien.MoveByWaypoints(_waypoints[Random.Range(0, _waypoints.Count)]._point);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private Alien SpawnEnemy(Alien alien)
    {
        GameObject newAlien = Instantiate(alien.gameObject, transform.position,quaternion.identity);
        return newAlien.GetComponent<Alien>();
    }

}

[Serializable]
public struct Waypoints
{
    public List<Transform> _point;
}
