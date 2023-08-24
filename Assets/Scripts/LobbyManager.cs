using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.Demo.Cockpit;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomInputField;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TextMeshProUGUI _roomName;

    [SerializeField] private RoomItem _roomItemPrefab;
    [SerializeField] private Transform _contentObject;
    private List<RoomItem> _roomItemsList = new List<RoomItem>();

    private float _timeBetweenUpdates = 1.5f;
    private float _nextUpdateTime;

    private int _minPlayersCount = 2;

    private List<PlayerItem> _playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem _playerItemPrefab;
    [SerializeField] private Transform _playerItemParent;

    [SerializeField] private GameObject _playButton;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        if(_roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(_roomInputField.text, new RoomOptions() { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
        }
    }

    public override void OnJoinedRoom()
    {
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= _nextUpdateTime)
        {
            UpdateRoomList(roomList);
            _nextUpdateTime = Time.time + _timeBetweenUpdates;
        }

    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(RoomItem item in _roomItemsList)
        {
            Destroy(item.gameObject);
        }    
        _roomItemsList.Clear();

        foreach(RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(_roomItemPrefab, _contentObject);
            newRoom.SetRoomName(room.Name.ToString());
            _roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        _roomPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem item in _playerItemsList)
            Destroy(item.gameObject);

        _playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
            return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(_playerItemPrefab, _playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            _playerItemsList.Add(newPlayerItem);
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= _minPlayersCount)
            _playButton.SetActive(true);
        else
            _playButton.SetActive(false);
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
