using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashCategory binCategory;  // Tipo de lixo que essa lixeira aceita
    public int moneyReward = 10;       // Quantidade de dinheiro que o jogador recebe por descartar o lixo correto

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Verifica se � o jogador que est� na lixeira
        {
            PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
            if (playerInventory != null && playerInventory.HasTrash())
            {
                // Percorre todos os itens do invent�rio do jogador
                for (int i = 0; i < playerInventory.GetInventoryCount(); i++)
                {
                    GameObject trashItem = playerInventory.GetTrashAt(i);  // Pega o lixo em cada posi��o do invent�rio
                    TrashType trashType = trashItem.GetComponent<TrashType>();

                    if (trashType != null && trashType.GetTrashCategory() == binCategory)
                    {
                        // Descartar o lixo e dar dinheiro ao jogador
                        playerInventory.DiscardTrash(i);  // Passa o �ndice para descartar o lixo correto
                        playerInventory.AddMoney(moneyReward);
                        Debug.Log("Lixo descartado corretamente! Voc� recebeu: " + moneyReward + " moedas.");
                        break;  // Sai do loop depois de descartar o lixo correto
                    }
                    else
                    {
                        Debug.Log("Este lixo n�o � o correto!");
                    }
                }
            }
        }
    }
}
