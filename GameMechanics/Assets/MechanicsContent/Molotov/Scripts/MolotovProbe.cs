using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class MolotovProbe : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private GameObject _firePrefab;
    [SerializeField]
    private float _spawnFireDistance = 0.2f;
    [SerializeField]
    private float _maxMovingDistannce = 2f;
    [SerializeField]
    private float _movingSpeed = 1f;
    [SerializeField]
    private float _drag = 0.1f;
    [SerializeField]
    private float _maxDownCast = 4f;


    [SerializeField]
    private UnityEvent _onStopMoving;

    [Header("Inspec")]
    [SerializeField]
    private float _movedDistance;
    [SerializeField]
    private float _remainDistanceToNextFire = 0f;

    private Rigidbody _rb;

    public void SetMovingSpeed(float movingSpeed)
    {
        _movingSpeed = movingSpeed;
    }

    private IDisposable _movingUpdateStream;
    public void StartMoving()
    {
        if (_movingUpdateStream != null)
        {
            _movingUpdateStream.Dispose();
        }

        _movedDistance = 0f;
        float currentSpeed = _movingSpeed;
        Vector3 startPos = _rb.position;

        _movingUpdateStream = Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (_remainDistanceToNextFire <= 0f)
            {
                SpawnFireCast();
                _remainDistanceToNextFire = _spawnFireDistance;
            }

            if (_movedDistance >= _maxMovingDistannce || currentSpeed <= 0f)
            {
                _movingUpdateStream.Dispose();
                StopMoving();
                return;
            }

            currentSpeed -= _drag * currentSpeed * Time.fixedDeltaTime;
            transform.position += transform.forward * currentSpeed * Time.fixedDeltaTime;

            _movedDistance += currentSpeed * Time.fixedDeltaTime;
            _remainDistanceToNextFire -= currentSpeed * Time.fixedDeltaTime;

            var ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out var hit, _maxDownCast))
            {
                transform.position = hit.point + Vector3.up * 0.1f;
            }
        });
    }

    private void SpawnFireCast()
    {
        var molotovCast = Instantiate(_firePrefab, transform.position, Quaternion.identity);
        molotovCast.GetComponent<MolotovFireCast>().CastAndSpawnFire();
    }

    private void StopMoving()
    {
        _onStopMoving.Invoke();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
