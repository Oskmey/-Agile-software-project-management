using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    string text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>().text;
    }
    public void OpenChannel()
    {   
       Application.OpenURL(text);
    }
    public void OpenChannelLinked(string link) 
    {
        Application.OpenURL(link);
    }
}
