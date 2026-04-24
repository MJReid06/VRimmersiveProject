using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI winText;

    private int completedObjectives = 0;
    private int totalObjectives = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
        winText.gameObject.SetActive(false); // hide win text at start
    }

    public void CompleteObjective()
    {
        completedObjectives++;
        UpdateUI();

        if (completedObjectives >= totalObjectives)
        {
            ShowWinMessage();
        }
    }

    private void UpdateUI()
    {
        objectiveText.text = completedObjectives + "/" + totalObjectives;
    }

    private void ShowWinMessage()
    {
        winText.gameObject.SetActive(true);
        winText.text = "All objectives complete!\nHead to the pier to escape the island!";
        Debug.Log("All objectives complete!");
    }
}