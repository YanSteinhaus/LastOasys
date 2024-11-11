using UnityEngine;
using System.Collections.Generic;
using TMPro;  // Required for TextMeshPro


public class PlayerInventory : MonoBehaviour
{
    public int maxSlots = 1;  // N�mero inicial de slots
    private List<GameObject> inventory = new List<GameObject>();  // Invent�rio
    private int playerMoney = 0;  // Dinheiro do jogador

    [SerializeField] private Transform headTransform;  // Transform da cabe�a do jogador
    [SerializeField] private float pickupRange = 2f;  // Raio para detectar o lixo
    [SerializeField] private float spacing = 0.5f;  // Dist�ncia entre os itens

    [SerializeField] private TMP_Text moneyText;  // Reference to the UI text element for displaying money


    void Update()
    {
        // Detecta lixos no alcance para pegar
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Trash") && hitCollider.transform.parent == null && inventory.Count < maxSlots)
            {
                PickUpTrash(hitCollider.gameObject);
                break;  // Pega um item de cada vez
            }
        }
    }

    void PickUpTrash(GameObject trash)
    {
        if (inventory.Count < maxSlots)
        {
            inventory.Add(trash);

            // Desabilitar f�sica para n�o cair (n�o usar gravidade)
            Rigidbody rb = trash.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Posicionar o lixo na cabe�a do jogador
            Vector3 newPosition = headTransform.position + new Vector3(0, inventory.Count * spacing, 0);
            trash.transform.position = newPosition;

            trash.transform.SetParent(headTransform);  // Seguir o jogador
            trash.SetActive(true);  // Garantir que o lixo esteja vis�vel
        }
    }

    // Checa se o jogador tem lixo
    public bool HasTrash()
    {
        return inventory.Count > 0;
    }

    // Retorna o n�mero de itens no invent�rio
    public int GetInventoryCount()
    {
        return inventory.Count;
    }

    // Retorna o lixo de uma posi��o espec�fica no invent�rio
    public GameObject GetTrashAt(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            return inventory[index];
        }
        return null;
    }

    // Descartar lixo (libera o slot)
    public void DiscardTrash(int index)
    {
        if (index >= 0 && index < inventory.Count)
        {
            GameObject discardedTrash = inventory[index];
            inventory.RemoveAt(index);  // Remove o lixo do invent�rio
            Destroy(discardedTrash);  // Destr�i o lixo

            // Agora a posi��o no invent�rio est� "vazia", ent�o um novo item pode ser colocado no slot

            // Atualiza a exibi��o do invent�rio, caso tenha uma interface (UI)
            UpdateInventoryDisplay();
        }
    }

    // Adicionar dinheiro ao jogador
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Dinheiro do jogador: " + playerMoney);
        UpdateMoneyUI();  // Update the money display in the UI

    }

    // Atualiza a exibi��o do invent�rio (UI)
    void UpdateInventoryDisplay()
    {
        // Aqui voc� pode adicionar um c�digo para atualizar a UI se necess�rio
        // Se houver uma barra de itens ou algo similar, ela pode refletir a mudan�a
    }

    // Fun��o para comprar upgrades
    public void UpgradeInventory()
    {
        maxSlots++;  // Aumenta o n�mero de slots
    }

    // Updates the money UI text to show the current balance
    void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "$: " + playerMoney.ToString();
        }
    }

    // Methods to add or spend money
    public int GetMoney() { return playerMoney; }

    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }

}
