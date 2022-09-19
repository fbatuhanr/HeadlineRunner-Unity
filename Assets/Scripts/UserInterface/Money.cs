using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;


    public string GetMoneyText()
    {
        return moneyText.text;
    }
    public void SetMoneyText(string text)
    {
        moneyText.text = text;
    }


}
