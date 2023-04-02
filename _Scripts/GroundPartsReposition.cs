using System;
using UnityEngine;

public class GroundPartsReposition : MonoBehaviour
{
    [SerializeField] private float _groundDisappearPositionZ;
    [SerializeField] private float _groundRespawnPositionZ;     

    public static Action OnGroundMoved;    

   
    void Update()
    {
        if (transform.position.z < _groundDisappearPositionZ)
            ChangeGroundPosition();
    }
    private void ChangeGroundPosition()
    {
        transform.position = new Vector3(0, 0, _groundRespawnPositionZ);        
        OnGroundMoved?.Invoke();         
    }
}
