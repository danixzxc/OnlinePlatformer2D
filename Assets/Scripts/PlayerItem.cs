using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _playerName;

    [SerializeField] private Image _backgroundImage;

    [SerializeField] private Color _highlightColor;
    [SerializeField] private GameObject _leftArrowButton;
    [SerializeField] private GameObject _rightArrowButton;

    public ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private Sprite[] _avatars;

    private Player _player;

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
    }


    public void SetPlayerInfo(Player player)
    {
       _playerName.text = player.NickName;
        _player = player;
       // UpdatePlayerItem(_player);
        playerProperties["playerAvatar"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void ApplyLocalChanges()
    {
        _backgroundImage.color = _highlightColor;
        _leftArrowButton.SetActive(true);
        _rightArrowButton.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
            playerProperties["playerAvatar"] = _avatars.Length - 1;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == _avatars.Length - 1)
            playerProperties["playerAvatar"] = 0;
        else
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable hashTable)
    {
        if (_player == targetPlayer)
            UpdatePlayerItem(targetPlayer);
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            _playerAvatar.sprite = _avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else 
        {
            playerProperties["playerAvatar"] = 0;
        }

    }


}
