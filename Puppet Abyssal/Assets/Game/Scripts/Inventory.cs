using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public int coinsPickUp;
    public Text coinsCountText;
    public Text coinsPickUpText;

    public Text graphics;

    public static Inventory instance;

    private void Awake()
    {
        graphics.color = new Color(1f, 1f, 1f, 0f);
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    public void AddCoinsNumber(int count)
    {
        graphics.color = new Color(1f, 1f, 1f, 1f);
        coinsPickUp += count;
        coinsPickUpText.text ="+ "+ coinsPickUp.ToString();
        StartCoroutine(WaitAddCoins());
    }

    public IEnumerator WaitAddCoins()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(AddCoins());
    }

    public IEnumerator AddCoins()
    {
        while (coinsPickUp != 0 && coinsPickUp > 50)
        {
            coinsPickUp--;
            if (coinsPickUp == 0)
            {
                graphics.color = new Color(1f, 1f, 1f, 0f);
            }
            coinsPickUpText.text = "+ " + coinsPickUp.ToString();
            coinsCount++;
            coinsCountText.text = coinsCount.ToString();
            yield return new WaitForSeconds(0.001f);
        }

        while (coinsPickUp != 0 && coinsPickUp <= 50)
        {
            coinsPickUp--;
            if (coinsPickUp == 0)
            {
                graphics.color = new Color(1f, 1f, 1f, 0f);
            }
            coinsPickUpText.text = "+ " + coinsPickUp.ToString();
            coinsCount++;
            coinsCountText.text = coinsCount.ToString();
            yield return new WaitForSeconds(0.05f);
        }

    }
}
