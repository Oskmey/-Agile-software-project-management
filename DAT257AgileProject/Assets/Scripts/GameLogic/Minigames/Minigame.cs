using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    protected abstract void StartMinigame();

    protected abstract void DestroyMinigame();
}
