using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviourPunCallbacks
{
    private PhotonView _view;
    private PlayerController _playerController;
    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Bullet") && _view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            _playerController = other.GetComponent<PlayerController>();
            _playerController.IncreaseCoins(1);
        }
    }
}
