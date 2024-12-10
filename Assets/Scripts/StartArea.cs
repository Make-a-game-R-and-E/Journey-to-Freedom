using UnityEngine;

public class StartArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check win condition
            GameManager.Instance.CheckWinCondition(collision);
        }
    }
}
