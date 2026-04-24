using UnityEngine;
using System.Collections;

public class CaveEntrance : MonoBehaviour
{
    [SerializeField] private GameObject doorMesh;
    [SerializeField] private float slideSpeed = 2f;
    [SerializeField] private float dropDistance = 3f;
    [SerializeField] private AudioClip doorSound;

    private AudioSource audioSource;
    private bool triggered = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.name);

        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(SealEntrance());
        }
    }

    private IEnumerator SealEntrance()
    {
        if (audioSource != null && doorSound != null)
            audioSource.PlayOneShot(doorSound);

        Vector3 startPos = doorMesh.transform.position;
        Vector3 targetPos = startPos + new Vector3(0, -dropDistance, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * slideSpeed;
            doorMesh.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        doorMesh.transform.position = targetPos;
    }
}