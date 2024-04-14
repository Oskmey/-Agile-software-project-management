using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MinigameOutcomeEvent : MonoBehaviour
{
    public UnityEvent onMinigameWon;
    public UnityEvent onMinigameLost;
}