using UnityEngine;
using System.Collections;

public class LeverPull : MonoBehaviour
{
    [Header("Rock Settings")]
    [SerializeField] private GameObject rock;
    [SerializeField] private float rockMoveDistance = 3f;
    [SerializeField] private float rockMoveSpeed = 2f;

    [Header("Door Settings")]
    [SerializeField] private GameObject door;
    [SerializeField] private float doorDelay = 5f;
    [SerializeField] private float doorOpenSpeed = 90f;
    [SerializeField] private float doorOpenAngle = 90f;

    [Header("Lever Settings")]
    [SerializeField] private float activationAngle = -20f;

    private new HingeJoint hingeJoint;
    private bool activated = false;

    private void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (activated) return;

        float angle = hingeJoint.angle;

        if (angle <= activationAngle)
        {
            activated = true;
            StartCoroutine(MoveRock());
            StartCoroutine(OpenDoorDelayed());

            if (ObjectiveManager.Instance != null)
                ObjectiveManager.Instance.CompleteObjective();
        }
    }

    private IEnumerator MoveRock()
    {
        Vector3 startPos = rock.transform.position;
        Vector3 targetPos = startPos + Vector3.right * rockMoveDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rockMoveSpeed;
            rock.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        rock.transform.position = targetPos;
    }

    private IEnumerator OpenDoorDelayed()
    {
        yield return new WaitForSeconds(doorDelay);

        Vector3 hingePoint = door.transform.position + door.transform.right * -0.5f;

        float rotated = 0f;
        while (rotated < doorOpenAngle)
        {
            float step = doorOpenSpeed * Time.deltaTime;
            door.transform.RotateAround(hingePoint, Vector3.up, step);
            rotated += step;
            yield return null;
        }
    }
}