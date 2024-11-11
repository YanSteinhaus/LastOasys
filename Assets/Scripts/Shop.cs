using UnityEngine;
using TMPro;
using System.IO;

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public StreamBehavior stream;
    public TextMeshProUGUI moneyText;

    private int streamHealthRefillCost = 4;
    private int streamHealthIncreaseCost = 4;
    private int inventoryUpgradeCost = 4;
    private int speedUpgradeCost = 4;

    void Start()
    {
        
        UpdateMoneyText();
    }

    public void RefillStreamHealth()
    {
        if (playerInventory.SpendMoney(streamHealthRefillCost))
        {
            stream.streamHealth += 5;  // Aumenta a vida do rio
            streamHealthRefillCost *= 2;  // Dobra o pre�o
            UpdateMoneyText();
        }
    }

    public void IncreaseStreamHealth()
    {
        if (playerInventory.SpendMoney(streamHealthIncreaseCost))
        {
            stream.streamHealth += 10;  // Aumenta permanentemente a vida m�xima
            streamHealthIncreaseCost *= 2;  // Dobra o pre�o
            UpdateMoneyText();
        }
    }

    public void UpgradeInventorySpace()
    {
        if (playerInventory.SpendMoney(inventoryUpgradeCost))
        {
            playerInventory.maxSlots++;  // Aumenta um slot de invent�rio
            inventoryUpgradeCost *= 2;  // Dobra o pre�o
            UpdateMoneyText();
        }
    }

    public void IncreasePlayerSpeed()
    {
        if (playerInventory.SpendMoney(speedUpgradeCost))
        {
            GetComponent<Movement>().moveSpeed += 1f;  // Aumenta a velocidade do player
            speedUpgradeCost *= 2;  // Dobra o pre�o
            UpdateMoneyText();
        }
    }

    // Atualiza o texto do dinheiro na interface do usu�rio
    void UpdateMoneyText()
    {
        moneyText.text = "Money: " + playerInventory.GetMoney();
    }
}
