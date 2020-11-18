using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public int coinValue;

    public GameObject objectToDetroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Inventory.instance.AddCoinsNumber(coinValue);
            Destroy(objectToDetroy);
        }
    }
}
