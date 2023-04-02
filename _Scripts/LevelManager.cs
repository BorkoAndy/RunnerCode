using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _startingSpeed;
    [SerializeField] private float _speedIncreasement;
    [SerializeField] private int _groundsChangeToNextLevel;
    [SerializeField] private GameObject[] _playerCharacters;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TextMeshProUGUI _endScoreText;

    private int _groundsChanged = 0;
    private int _chosenPlayer;

    public static float _speed;
    public static Action OnLevelIncrease;
    private void OnEnable()
    {
        GroundPartsReposition.OnGroundMoved += IncreaseLevel;
        PlayerMovement.OnPlayerDies += ShowEndScreen;
    }   

    private void OnDisable()
    {
        GroundPartsReposition.OnGroundMoved -= IncreaseLevel;
        PlayerMovement.OnPlayerDies -= ShowEndScreen;
    }
    private void Awake()
    {
        _endScreen.SetActive(false);
        foreach (var player in _playerCharacters)
        {
            player.SetActive(false);
        }
        _chosenPlayer = StartScene._chosenPlayer;
        _playerCharacters[_chosenPlayer].SetActive(true);   
        
        _speed = _startingSpeed;
    }
    private void IncreaseLevel()
    {
        _groundsChanged++;
        if (_groundsChanged >= _groundsChangeToNextLevel)
        {
            _speed += _speedIncreasement;
            _groundsChanged = 0;
            OnLevelIncrease?.Invoke();
        }


    }
    private void ShowEndScreen()
    {
        _endScreen.SetActive(true);
        _endScoreText.text = CoinCounter._coinsAmount.ToString();
    }
    public void RestartButton()
    {
        CoinCounter._coinsAmount = 0;
        _endScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
