using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningPopup : MonoBehaviour
{
    public float displayTime = 2.0f; // The time the warning will be displayed
    [SerializeField]
    private TextMeshProUGUI warningText;

    void Start()
    {
        warningText.enabled = false; // Hide the warning text initially
    }

    public void DisplayWarning(string message)
    {
        warningText.text = message;
        warningText.enabled = true;
        StartCoroutine(HideWarningAfterTime());
    }

    private IEnumerator HideWarningAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        warningText.enabled = false;
    }
}
