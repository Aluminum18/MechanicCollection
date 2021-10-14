using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MolotovFireCast : MonoBehaviour
{
    [SerializeField]
    private GameObject _molotovFire;
    [SerializeField]
    private Vector2 _randomSpawnTime;
    [SerializeField]
    private float _maxDownFire = 2f;

    public void CastAndSpawnFire()
    {
        float spawnAfter = Random.Range(_randomSpawnTime.x, _randomSpawnTime.y);

        Observable.Timer(System.TimeSpan.FromSeconds(spawnAfter)).Subscribe(_ =>
        {
            var ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, out var hit, _maxDownFire))
            {
                return;
            }

            SpawnFire(hit.point);
        });
    }

    private void SpawnFire(Vector3 pos)
    {
        Instantiate(_molotovFire, pos, Quaternion.identity);
    }
}
