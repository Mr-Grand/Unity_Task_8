using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _time = 0.3f;
    
    private Coroutine _coroutine;
    private bool _isSpawnActive = true;

    private IEnumerator CubeCoroutine()
    {
        while (_isSpawnActive)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-6,6), 10, Random.Range(-6,6));
            GameObject obj = _objectPool.GetObject(randomPosition);
            yield return new WaitForSeconds(_time);
        }
    }

    private void Start()
    {
        _coroutine = StartCoroutine(CubeCoroutine());
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Излишний метод, но мне было удобно останавливать корутину для проверки
        {
            _isSpawnActive = !_isSpawnActive;

            if (_isSpawnActive)
            {
                _coroutine = StartCoroutine(CubeCoroutine());
            }
            if (!_isSpawnActive)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}
