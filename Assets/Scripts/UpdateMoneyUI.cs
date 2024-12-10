using System;
using TMPro;
using UnityEngine;
using YG;

public class UpdateMoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyAmountText;

    private void Start()
    {
        RefreshMoneyAmount();
    }

    public void RefreshMoneyAmount()
    {
        moneyAmountText.text = YG2.saves.AmountMoney.ToString();
    }
}
