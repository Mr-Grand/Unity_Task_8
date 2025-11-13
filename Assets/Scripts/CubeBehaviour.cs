using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private bool _isColoreChanged = false;
    [SerializeField] private float _jumpForce = 10f;

    private ObjectPool _pool;

    private void Awake()
    {
        SetUpCube();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!_isColoreChanged)
                ChangeColorStatus();
            
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpForce = Mathf.Clamp(_jumpForce/2, 3f, 10f);

            ReturnObjectWithDelay(gameObject);
        }
    }
    
    private void SetUpCube()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.white;
        _isColoreChanged = false;
    }
    
    public void Initialize(ObjectPool pool)
    {
        _pool = pool;
    }
    
    private void ChangeColorStatus()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = new Color(Random.value, Random.value, Random.value);
        _isColoreChanged = true;
    }
    
    private IEnumerator ReturnDelayCoroutine(GameObject obj)
    {
        float timeBeforeReturn = Random.Range(2f, 6f);
        yield return new WaitForSeconds(timeBeforeReturn);
        SetUpCube();
        _pool.ReturnObject(obj);
    }
    
    private void ReturnObjectWithDelay(GameObject obj)
    {
        StartCoroutine(ReturnDelayCoroutine(obj));
    }
}
