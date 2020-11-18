using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(loadnextScene());
        }
    }

    public IEnumerator loadnextScene()
    {
        fadeSystem.SetTrigger("FadeIN");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneName);
    }
}
