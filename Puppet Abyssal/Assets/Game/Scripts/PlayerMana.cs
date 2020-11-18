using UnityEngine.UI;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 10;
    public int currentMana;

    public int healthPoints = 1;

    private const float timeToCharge = 2f;
    private float chargeTime = 0.0f;

    public TextMesh chargeTimeText;

    public Text currentManaDisplay;
    public Text maxManaDisplay;

    public static PlayerMana instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMana dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        chargeTimeText.color = new Color(1f, 1f, 1f, 0f);

        currentMana = maxMana;
        currentManaDisplay.text = currentMana.ToString();
        maxManaDisplay.text = maxMana.ToString();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H) && (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth) && (currentMana >= 1) && (PlayerMovement.instance.isGrounded == true) && (PlayerDeath.instance.isDead == false))
        {
            PlayerMovement.instance.enabled = false;

            chargeTimeText.text = chargeTime.ToString("0.0");

            chargeTimeText.color = new Color(1f, 1f, 1f, 1f);
            chargeTime += Time.deltaTime;

            if (chargeTime >= timeToCharge)
            {
                PlayerMovement.instance.enabled = true;

                chargeTimeText.color = new Color(1f, 1f, 1f, 0f);
                chargeTime = 0;

                currentMana--;
                PlayerHealth.instance.HealPlayer(healthPoints);
                currentManaDisplay.text = currentMana.ToString();
            } 
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            PlayerMovement.instance.enabled = true;

            chargeTimeText.color = new Color(1f, 1f, 1f, 0f);
            chargeTime = 0;
        }
    }
}
