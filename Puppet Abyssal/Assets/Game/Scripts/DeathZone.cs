using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform playerRespawn;
    public Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        playerRespawn = GameObject.FindGameObjectWithTag("PlayerRespawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    public IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIN");
        yield return new WaitForSeconds(0.8f);
        collision.transform.position = playerRespawn.position;
    }
}
