using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubheaderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subheader;

    public void SetHeaderText(string factInfo)
    {
        subheader.text = $"{factInfo}";
    }
}

