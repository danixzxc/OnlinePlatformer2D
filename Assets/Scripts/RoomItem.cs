using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _roomName;
    private LobbyManager _manager;

    private void Start()
    {
        GameObject obj = GameObject.Find("LobbyManager");
        _manager = obj.GetComponent<LobbyManager>();
    }

    public void SetRoomName(string roomName)
    {
        _roomName.text = roomName;
    }

    public void OnClickItem()
    {
        _manager.JoinRoom(_roomName.text);
    }


}
