using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;
using Photon.Realtime;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private GameObject _bloodParticles;
    [SerializeField] private GameObject _player;
    private FixedJoystick _joystick;

    private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();


    private PhotonView _view;
    private CanvasController _canvasController;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        GameObject _object = GameObject.Find("Fixed Joystick");
        _joystick = _object.GetComponent<FixedJoystick>();

        GameObject canvas = GameObject.Find("Canvas");
        _canvasController = canvas.GetComponent<CanvasController>();
    }

    private void Start()
    {
        _playerProperties["playerCoins"] = 0;
        _playerProperties["isAlive"] = true;

        if (_view.IsMine)
        {
            _player.gameObject.tag = "Player";
        }
    }

    private void FixedUpdate()
    {
        if(_view.IsMine)
        {
            Vector3 input = new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);
            transform.position += input.normalized * _speed * Time.deltaTime;

            if (_joystick.Vertical < 0)
            {
                transform.eulerAngles = new Vector3(180, 0, _joystick.Horizontal * -90);
            }
            else if (_joystick.Vertical > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, _joystick.Horizontal * -90);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _canvasController.ChangeHealth(_health, 30);
        PhotonNetwork.Instantiate(_bloodParticles.name, transform.position, transform.rotation);
        if (_health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);

        PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] + 1;
        _playerProperties["isAlive"] = false;

        Debug.Log("you died");

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["playersAmount"] - 1)
            _canvasController.EndGame();
    }
    public void IncreaseCoins(int coins)
    {
        _playerProperties["playerCoins"] = (int)_playerProperties["playerCoins"] + coins;
        PhotonNetwork.CurrentRoom.CustomProperties["playerCoins"] = _playerProperties["playerCoins"];
    }

}
