using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public bool isDead = false;

    private Transform playerSpawn;
    public Animator fadeSystem;

    private Transform player;

    public static PlayerDeath instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;


        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Death()
    {
        isDead = true;

        // retirer des pièces
        Inventory.instance.coinsCount = 0;
        Inventory.instance.coinsCountText.text = Inventory.instance.coinsCount.ToString();

        // empêcher les interactions physique avec les autres éléments de la scéne
        PlayerMovement.instance.rb2d.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;

        // bloquer les mouvements du personnage
        PlayerMovement.instance.enabled = false;

        // jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Death");

        StartCoroutine(ReplaceDeadPlayer());

        RespawnPlayerAfterDeath();
    }

    public IEnumerator ReplaceDeadPlayer()
    {
        yield return new WaitForSeconds(0.4f);
        fadeSystem.SetTrigger("FadeIN");
        yield return new WaitForSeconds(1f);
        player.position = playerSpawn.position;
    }

    public void RespawnPlayerAfterDeath()
    {
        // reset Mana
        PlayerMana.instance.currentMana = PlayerMana.instance.maxMana;
        PlayerMana.instance.currentManaDisplay.text = PlayerMana.instance.currentMana.ToString();

        // reset HP
        PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
        PlayerHealth.instance.healthbar.SetHealth(PlayerHealth.instance.currentHealth);

        PlayerMovement.instance.rb2d.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;

        PlayerMovement.instance.enabled = true;
    }
}
