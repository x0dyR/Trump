using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trump
{
    public class EnemySpawner : MonoBehaviour
    {
        private Coroutine _spawn;
        [field: SerializeField] private List<Transform> _enemySpawnPoints;
        [field: SerializeField] private float _spawnCooldown = 2f;
        [field: SerializeField] private EnemyFabric _enemyFabric;

        private void Start()
        {
            // Для отладки запускаем спавн при старте
            StartWork();
        }

        public void StartWork()
        {
            StopWork();

            _spawn = StartCoroutine(Spawn());
        }

        public void StopWork()
        {
            if (_spawn != null)
            {
                StopCoroutine(_spawn);
                _spawn = null;
            }
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                EnemyType enemyType = (EnemyType)Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
                AbsEnemy enemy = _enemyFabric.Get(enemyType);
                if (enemy == null)
                {
                    Debug.LogError("Failed to get enemy from fabric!");
                    yield break;
                }
                Transform spawnPoint = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Count)];
                enemy.transform.position = spawnPoint.position;

                yield return new WaitForSeconds(_spawnCooldown);
            }
        }
    }
}
