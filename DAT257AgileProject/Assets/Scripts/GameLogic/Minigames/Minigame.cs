using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame : MonoBehaviour, IMinigame
{
    public delegate void MinigameEvent();

    public event MinigameEvent OnMinigameWon;
    public event MinigameEvent OnMinigameLost;

    protected string promptText;

    public string PromptText { get { return promptText; } }
    public abstract void StartMinigame();
    public abstract void DestroyMinigame();

    public abstract bool HandleCatch();
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
