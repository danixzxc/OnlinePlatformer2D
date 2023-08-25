using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            PhotonNetwork.Instantiate(_coinPrefab.name, spawnPoint.position, Quaternion.identity);
        }
    }
}
