using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class RecyclingInteraction : MonoBehaviour
{
    private bool isPlayerInRange;
    private TextMeshProUGUI promptText;

    public bool IsPlayerInRange
    {
        get
        {
            return isPlayerInRange;
        }
    }

    private void Awake()
    {
        promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (promptText != null)
            {
                promptText.text = "Press R to Recycle";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if(promptText != null)
            {
                promptText.text = "";
            }
        }
    }
}
