
using UnityEngine;

public class EnnemyKillSpots : MonoBehaviour
{
    public GameObject objectToDetroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(objectToDetroy);
        }
    }
}
