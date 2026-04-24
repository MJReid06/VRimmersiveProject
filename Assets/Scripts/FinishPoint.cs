using UnityEngine;
using System.Collections;
using TMPro;

public class FinishPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI congratsText;
    [SerializeField] private UnityEngine.UI.Image blackScreen;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(FinishGame());
        }
    }

    private IEnumerator FinishGame()
    {
        // Fade to black
        float t = 0f;
        Color c = blackScreen.color;
        c.a = 0f;
        blackScreen.color = c;
        blackScreen.gameObject.SetActive(true);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            blackScreen.color = c;
            yield return null;
        }

        // Show congrats text
        congratsText.gameObject.SetActive(true);
        congratsText.text = "Congratulations!\nYou escaped the island!";
    }
}