using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame : MonoBehaviour, IMinigame
{
    protected UnityEvent onMinigameWon;
    protected UnityEvent onMinigameLost;
    protected string promptText;

    public string PromptText { get { return promptText; } }
    public abstract void StartMinigame();
    public abstract void DestroyMinigame();
}
