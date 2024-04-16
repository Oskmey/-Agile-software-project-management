using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame : MonoBehaviour, IMinigame
{
    // Define the delegate for the events
    protected delegate void MinigameEvent();

    protected event MinigameEvent OnMinigameWon;
    protected event MinigameEvent OnMinigameLost;

    // protected UnityEvent onMinigameWon;
    // protected UnityEvent onMinigameLost;
    protected string promptText;

    public string PromptText { get { return promptText; } }
    public abstract void StartMinigame();
    public abstract void DestroyMinigame();

    // Methods to trigger the events
    protected virtual void MinigameWon()
    {
        OnMinigameWon?.Invoke();
    }

    protected virtual void MinigameLost()
    {
        OnMinigameLost?.Invoke();
    }
}
