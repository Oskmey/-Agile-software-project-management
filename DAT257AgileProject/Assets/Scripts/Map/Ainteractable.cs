using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Ainteractable : MonoBehaviour
{
    private Collider2D player;
    private TextMeshProUGUI promtText;
    public abstract string text { get;}

    public void Awake()
    {
        promtText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        if (promtText == null)
        {
            Debug.LogWarning("TutorialText not found");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && text != null)
        {
            player = collision;
            collision.gameObject.GetComponent<PlayerController>().AddInteractable(this);
            promtText.text = text;
        }
        else
        {
            Debug.LogWarning("Text is null");
        }
    }

    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            promtText.text = "";
            collision.gameObject.GetComponent<PlayerController>().RemoveInteractable(this);
        }
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

    public abstract void interact();
}
