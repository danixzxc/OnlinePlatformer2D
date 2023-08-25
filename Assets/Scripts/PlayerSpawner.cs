using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        int playerNumber = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];
        Transform spawnPoint = spawnPoints[playerNumber];
        GameObject playerToSpawn = playerPrefabs[playerNumber];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }

}
