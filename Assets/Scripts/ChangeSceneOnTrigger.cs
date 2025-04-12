using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    // Opcjonalnie mo¿esz ustawiæ opóŸnienie przed zmian¹ sceny (w sekundach)
    [SerializeField] private float delay = 0f;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            if (delay > 0f)
                StartCoroutine(LoadSceneWithDelay());
            else
                SceneManager.LoadScene(sceneToLoad);
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
