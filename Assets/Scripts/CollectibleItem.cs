using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemType = "Bread"; // Could be extended to multiple types

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Let the GameManager know that this bread is collected
            GameManager.Instance.CollectItem(itemType);
            Destroy(gameObject);
        }
    }
}
