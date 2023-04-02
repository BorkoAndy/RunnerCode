using System;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float _zBorder;

    public static Action<GameObject> ReturnToPool;
    private void Start() => _zBorder = GameObject.FindGameObjectWithTag("Player").transform.position.z - 10f;
    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * LevelManager._speed);

        if (transform.position.z < _zBorder)
            ReturnToPool?.Invoke(this.gameObject);
    }
}
