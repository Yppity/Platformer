using TMPro;
using UnityEngine;

[RequireComponent (typeof(TextMeshPro))]

public class DisplayCollector : MonoBehaviour
{
    [SerializeField] private Collector _collector;

    private TextMeshPro _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        _collector.NumberCoinsChanget += UpdateCoinDisplay;
    }

    private void OnDisable()
    {
        _collector.NumberCoinsChanget -= UpdateCoinDisplay;
    }

    private void UpdateCoinDisplay(int number)
    {
        _text.text = number.ToString();
    }
}
