using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private GameObject _bloodParticles;
    [SerializeField] private GameObject _player;
    private FixedJoystick _joystick;

    private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();


    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        GameObject _object = GameObject.Find("Fixed Joystick");
        _joystick = _object.GetComponent<FixedJoystick>();
    }

    private void Start()
    {
        _playerProperties["playerCoins"] = 0;
        _playerProperties["isAlive"] = true;
        _player.gameObject.tag = "Player";
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
        PhotonNetwork.Instantiate(_bloodParticles.name, transform.position, transform.rotation);
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        PhotonNetwork.Destroy(gameObject);
        PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] + 1;
        _playerProperties["isAlive"] = false;

        Debug.Log("you died");

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["deadPlayersAmount"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["playersAmount"] - 1)
            //GameEndScreen();
            Debug.Log("game end");
    }
    public void IncreaseCoins(int coins)
    {
        _playerProperties["playerCoins"] = (int)_playerProperties["playerCoins"] + coins;
    }
}
