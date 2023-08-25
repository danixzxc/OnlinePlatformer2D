using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletParticles;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PhotonNetwork.Instantiate(_bulletPrefab.name, _shootingPoint.position, transform.rotation);
            PhotonNetwork.Instantiate(_bulletParticles.name, transform.position, transform.rotation);
        }
    }
}
