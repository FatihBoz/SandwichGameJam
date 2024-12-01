using UnityEngine;

/// <summary>
/// Tutorial Manager to handle multiple tutorials
/// </summary>
public class TutorialManager : MonoBehaviour
{

    private CycleManager cycleManager;

    [SerializeField] private TutorialBase[] tutorials;
    
    private int currentTutorialIndex = 0;

    private void Start()
    {
        cycleManager = FindObjectOfType<CycleManager>();
        cycleManager.SetIsStopped(true);

        // Subscribe to tutorial completion events
        foreach (var tutorial in tutorials)
        {
            tutorial.OnTutorialCompleted += MoveToNextTutorial;
        }

        // Start first tutorial
        StartFirstTutorial();
    }

    private void StartFirstTutorial()
    {
        if (tutorials.Length > 0)
        {
            tutorials[currentTutorialIndex].StartTutorial();
        }
    }

    private void MoveToNextTutorial()
    {
        // Move to next tutorial
        currentTutorialIndex++;

        // Check if all tutorials are completed
        if (currentTutorialIndex < tutorials.Length)
        {
            tutorials[currentTutorialIndex].StartTutorial();
        }
        else
        {
            // All tutorials completed
            Debug.Log("All Tutorials Completed!");
        }
    }

    // Optional: Method to restart tutorials
    public void RestartTutorials()
    {
        currentTutorialIndex = 0;
        StartFirstTutorial();
    }
}
