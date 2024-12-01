using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Base class for different types of tutorials in the game
/// </summary>

[Serializable]
public abstract class TutorialBase : MonoBehaviour
{
    [SerializeField] protected string tutorialName;
    [SerializeField] protected string tutorialDescription;
    
    [Header("UI References")]
    [SerializeField] public GameObject tutorialPanel;
    [SerializeField] public TMP_Text descriptionText;
    
    // Event triggered when tutorial is completed
    public event Action OnTutorialCompleted;
    
    // Indicates if the tutorial has been completed
    protected bool isCompleted = false;
    
    // Indicates if the tutorial is currently active
    protected bool isActive = false;

    /// <summary>
    /// Starts the tutorial
    /// </summary>
    public virtual void StartTutorial()
    {
        isActive = true;
        tutorialPanel.SetActive(true);
        descriptionText.text = tutorialDescription;
        
        // Additional setup for specific tutorial type
        SetupTutorial();
    }

    /// <summary>
    /// Abstract method to be implemented by specific tutorial types
    /// Sets up unique conditions for tutorial completion
    /// </summary>
    protected abstract void SetupTutorial();

    /// <summary>
    /// Checks if tutorial conditions are met
    /// </summary>
    protected abstract bool CheckTutorialCompletion();

    /// <summary>
    /// Completes the tutorial
    /// </summary>
    protected virtual void CompleteTutorial()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            isActive = false;
            tutorialPanel.SetActive(false);
            
            // Invoke completion event
            OnTutorialCompleted?.Invoke();
        }
    }

    /// <summary>
    /// Cancels the current tutorial
    /// </summary>
    public virtual void CancelTutorial()
    {
        isActive = false;
        tutorialPanel.SetActive(false);
    }
}
