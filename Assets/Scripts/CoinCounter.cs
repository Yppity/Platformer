using TMPro;
using UnityEngine;

[RequireComponent (typeof(TextMeshPro))]

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private TextMeshPro _text;
    private int _coinsCount = 0;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        _coinSpawner.CoinCollected += UpdateCoinDisplay;
    }

    private void OnDisable()
    {
        _coinSpawner.CoinCollected -= UpdateCoinDisplay;
    }

    private void UpdateCoinDisplay()
    {
        _coinsCount++;
        _text.text = _coinsCount.ToString();
    }
}
