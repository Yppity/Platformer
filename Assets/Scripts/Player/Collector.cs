using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _numberCoins = 0;

    public event Action<int> NumberCoinsChanget;
    public event Action<int> MedicineChestCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            if (coin.IsCollected == false)
            {
                CoinCollected();
                coin.Destroy();
            }

        }
        else if (collision.TryGetComponent(out MedicineChest medicineChest))
        {
            if (medicineChest.IsCollected == false)
            {
                MedicineChestCollected?.Invoke(medicineChest.Value);
                medicineChest.Destroy();
            }
        }
    }

    private void CoinCollected()
    {
        _numberCoins++;
        NumberCoinsChanget?.Invoke(_numberCoins);
    }
}
