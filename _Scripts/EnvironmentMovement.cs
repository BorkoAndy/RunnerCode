using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    private float _speed;   
   
    private void Update()
    {
        _speed = LevelManager._speed;
        transform.position += Vector3.back * _speed * Time.deltaTime;       
    }    
}
