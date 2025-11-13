using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _poolSize;
    
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_objectPrefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 randomPosition)
    {
        var obj = _objectPool.Count > 0 ? _objectPool.Dequeue() : Instantiate(_objectPrefab);
        CubeBehaviour cubeBehaviour = obj.GetComponent<CubeBehaviour>();
        cubeBehaviour.Initialize(this); // Передача ссылки на пул
        obj.transform.SetPositionAndRotation(Vector3.zero + randomPosition, Random.rotation);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
    }
}
