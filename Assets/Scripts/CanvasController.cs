using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private Image _healthBar;

    private ExitGames.Client.Photon.Hashtable _playerProperties = new ExitGames.Client.Photon.Hashtable();
    public void EndGame()
    {
        Time.timeScale = 0f;
        _victoryPanel.SetActive(true);

        _victoryText.text = "Thanks for playing! You collected " + PhotonNetwork.CurrentRoom.CustomProperties["playerCoins"] + "coins!";
    }

    public void ChangeHealth(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }
}
