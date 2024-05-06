using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningPopup : MonoBehaviour
{
    public float displayTime = 2.5f; // The time the warning will be displayed
    [SerializeField]
    private TextMeshProUGUI warningText;

    void Start()
    {
        gameObject.SetActive(false);
        warningText.enabled = false;
    }

    public void DisplayWarning(string message)
    {
        warningText.enabled = true;
        gameObject.SetActive(true);
        warningText.text = message;
        StartCoroutine(HideWarningAfterTime());
    }

    private IEnumerator HideWarningAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        warningText.text = "";
        gameObject.SetActive(false);
        warningText.enabled = false;
    }
}
