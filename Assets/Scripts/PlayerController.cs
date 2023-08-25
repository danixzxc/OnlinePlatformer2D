using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private GameObject _bloodParticles;
    // [SerializeField] private Animator _animator;

    //[SerializeField] private ScoreManager _scoreManager;
    //[SerializeField] private GameObject _coin;

    private PhotonView _view;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
        _view = GetComponent<PhotonView>();
        //_scoreManager = GetComponent<ScoreManager>();
    }

    private void Update()
    {
        if(_view.IsMine)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            transform.position += input.normalized * _speed * Time.deltaTime;

           // if (input == Vector3.zero)
               // _animator.SetBool("IsRunning", false);

            //else
                //_animator.SetBool("IsRunning", true);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        PhotonNetwork.Instantiate(_bloodParticles.name, transform.position, transform.rotation);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
