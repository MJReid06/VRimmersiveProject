using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    private bool ballScored = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ballScored) return;

        if (other.CompareTag("Ball"))
        {
            ballScored = true;
            ObjectiveManager.Instance.CompleteObjective();
            Debug.Log("Ball in basket!");
            Destroy(other.gameObject, 0.5f);
        }
    }
}