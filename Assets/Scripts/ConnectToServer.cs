using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public void OnClickConnect()
    {
        if(_usernameInput.text.Length >= 1)
        { 
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
    
}
