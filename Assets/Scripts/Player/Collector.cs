using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _numberCoins = 0;

    public Action<int> NumberCoinsChanget;

    public void CoinCollected()
    {
        _numberCoins++;
        NumberCoinsChanget?.Invoke(_numberCoins);
    }
}
