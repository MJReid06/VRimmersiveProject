using UnityEngine;
using System.Collections;

public class TeleportPad : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float cooldown = 3f;

    private bool isReady = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Telepad hit by: " + other.name);

        if (!isReady) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        isReady = false;

        TeleportPad destinationPad = destination.GetComponent<TeleportPad>();
        if (destinationPad != null) destinationPad.DisablePad(cooldown);

        
        Transform root = player.transform.root;
        root.position = destination.position;

        Debug.Log("Teleported!");

        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    public void DisablePad(float duration)
    {
        StartCoroutine(DisableRoutine(duration));
    }

    private IEnumerator DisableRoutine(float duration)
    {
        isReady = false;
        yield return new WaitForSeconds(duration);
        isReady = true;
    }
}