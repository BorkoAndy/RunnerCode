using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCounter;

    public static int _coinsAmount = 0;
    private void OnEnable() => CoinMovement.OnCoinCollected += AddCoins;
    private void OnDisable() => CoinMovement.OnCoinCollected -= AddCoins;
    private void Start() => _coinCounter.text = _coinsAmount.ToString();
    private void AddCoins()
    {
        _coinsAmount++;
        _coinCounter.text = _coinsAmount.ToString();
    }
}
