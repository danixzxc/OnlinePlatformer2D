using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
