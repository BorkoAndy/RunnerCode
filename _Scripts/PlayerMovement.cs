using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] private int _maximumHits;
    
    private bool _canJump = true;
    private int _hitCount;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public static Action OnPlayerDies;

    private void Start()
    {
        _hitCount = 0;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }
        Vector2 startSwipePosition;
        Vector2 endSwipePosition;
    private void Update()
    {
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)            
                startSwipePosition = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                endSwipePosition = touch.position;

                if (startSwipePosition.y < endSwipePosition.y && touch.deltaPosition.y > touch.deltaPosition.x)
                {                    
                    Jump();
                    return;
                }
                
                if (startSwipePosition.x > endSwipePosition.x )
                {
                    MoveLeft();
                    return;
                }

                if (startSwipePosition.x < endSwipePosition.x)
                {
                    MoveRight();
                    return;
                }
            }            
        }

        

        if (Input.GetKeyDown(KeyCode.A))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            MoveRight();
        else if (Input.GetKeyDown(KeyCode.W))
            Jump();

        if (_rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * 4 * Time.deltaTime;
    }

    private void Jump()
    {
        if (_canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    private void MoveRight()
    {
        if (transform.position.x < 1)
            transform.Translate(Vector3.right);
        _animator.SetTrigger("GoRight");
    }

    private void MoveLeft()
    {
        if (transform.position.x > -1)
            transform.Translate(Vector3.left);
        _animator.SetTrigger("GoLeft");
    }
    private void OnCollisionEnter(Collision collision)
    {
        _canJump = true;
        if (collision.gameObject.layer == 7)
        {
            collision.collider.isTrigger = true;
            _hitCount++;
            if( _hitCount >= _maximumHits ) 
            {
                _animator.SetTrigger("CriticalHit");
                OnPlayerDies?.Invoke();
                return;
            }
            _animator.SetTrigger("Hit");            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _canJump = false;
    }
}
