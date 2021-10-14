using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Molotov : MonoBehaviour
{
    [SerializeField]
    private int _probeNumber = 20;
    [SerializeField]
    private float _probeForward = 0.1f;
    [SerializeField]
    private GameObject _probe;
    [SerializeField]
    private float _maxUpCast = 0.5f;

    private BoxCollider _collider;
    private Vector3 _startProbe;

    public void Explode()
    {
        _collider.enabled = false;
        _startProbe = transform.position + Vector3.up * _maxUpCast;
        var ray = new Ray(transform.position, Vector3.up);
        if (Physics.Raycast(ray, out RaycastHit hit, _maxUpCast))
        {
            _startProbe = hit.point;
        }

        float angleOffset = 360f / _probeNumber;
        for (int i = 0; i < _probeNumber; i++)
        {
            Vector3 direction = new Vector3(0f, i * angleOffset, 0f);
            var probe = Instantiate(_probe, _startProbe, Quaternion.Euler(direction));
            probe.GetComponent<MolotovProbe>().StartMoving();
        }
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }
}
