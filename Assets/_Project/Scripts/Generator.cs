using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _objectToGenerate;
    [SerializeField] private float _delay;
    private Coroutine _generatorRoutine;
    private void Start()
    {
        _generatorRoutine = StartCoroutine(Spawn());
        Actions.OnFinish += OnFinish;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(_objectToGenerate, transform);
            yield return new WaitForSeconds(_delay);
        }
    }

    private void OnFinish()
    {
        StopCoroutine(_generatorRoutine);   
    }

    private void OnDestroy()
    {
        Actions.OnFinish -= OnFinish;
    }
}
