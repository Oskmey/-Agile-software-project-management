using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    private bool isPlayerInRange;
    private TextMeshProUGUI promtText;


    private void Awake()
    {
        promtText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    public bool IsPlayerInRange
    {
        get
        {
            return isPlayerInRange;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            promtText.text = "Press E to Shop";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            promtText.text = "";
        }
    }
}
