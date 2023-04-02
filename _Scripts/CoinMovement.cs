using System;
using System.Collections;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private Transform _coinCounter;
    private bool _isCollected = false;
    private float _zBorder;

    public static event Action OnCoinCollected;
    public static Action<GameObject> ReturnCoinToPool;

    private void Start()
    {
        _zBorder = GameObject.FindGameObjectWithTag("Player").transform.position.z - 10f;
        _coinCounter = GameObject.FindGameObjectWithTag("Counter").transform;
    }
    void Update()
    {
        if (!_isCollected)
            transform.Translate(Vector3.back * Time.deltaTime * LevelManager._speed);
        if (transform.position.z < _zBorder)           
            ReturnToPool();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isCollected = true;
            StartCoroutine(CollectCoin());
            OnCoinCollected?.Invoke();
        }       
    }

   private IEnumerator CollectCoin()
    {
        float time = 0;
        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, _coinCounter.position, time);
            time += Time.deltaTime * 0.01f;            
            
            if (Vector3.Distance(transform.position, _coinCounter.position) < 1000f)                         
               ReturnToPool();                
            
            yield return new WaitForSeconds(0.02f);
        }       
    }
    private void ReturnToPool() => ReturnCoinToPool?.Invoke(this.gameObject);
}
