using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class ShootingController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Transform _centerRotation;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletParticles;

    private PhotonView _view;
    private Button _button;
    private void Awake()
    {
        GameObject _object = GameObject.Find("Fire Button");
        _button = _object.GetComponent<Button>();
        _button.onClick.AddListener(Fire);
        _view = GetComponent<PhotonView>();
    }

    private void Fire()
    {
        if (_view.IsMine)
        {
            PhotonNetwork.Instantiate(_bulletPrefab.name, _shootingPoint.position, this.transform.rotation);
            PhotonNetwork.Instantiate(_bulletParticles.name, transform.position, this.transform.rotation);
        }
    }
}
