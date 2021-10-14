using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explodable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onExplode;

    public void Explode()
    {
        _onExplode.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
