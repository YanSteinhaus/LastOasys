using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Posi��o fixa do rio
    [SerializeField]
    private Vector3 riverPosition = new Vector3(0, 0, 0); // Defina a posi��o no Inspetor

    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        // Obt�m o Rigidbody do inimigo
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move em dire��o � posi��o fixa do rio
        Vector3 directionToRiver = (riverPosition - transform.position).normalized;
        rb.MovePosition(transform.position + directionToRiver * speed * Time.fixedDeltaTime);
    }
}
