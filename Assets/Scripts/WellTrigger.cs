using UnityEngine;

public class WellTrigger : MonoBehaviour
{
    private bool coinDropped = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Well trigger hit by: " + other.name + " tag: " + other.tag);

        if (coinDropped) return;

        if (other.CompareTag("Coin"))
        {
            coinDropped = true;
            Debug.Log("Coin in well! Objective complete!");
            Destroy(other.gameObject, 0.5f);

            if (ObjectiveManager.Instance != null)
                ObjectiveManager.Instance.CompleteObjective();
        }
    }
}