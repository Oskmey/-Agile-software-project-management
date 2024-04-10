using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowBoxMinigame : MonoBehaviour, IMinigame
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject boxPrefab;
    GameObject tutorialText;
    GameObject arrow;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GameObject.FindGameObjectWithTag("TutorialText");
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMinigame()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);

        tutorialText.GetComponent<TextMeshProUGUI>().text = "press spacebar to catch";
    }

    public void DestroyMinigame() // Runs on both "Success" and "Very Bad!"
    {    
        if (box != null)
        {
            Destroy(box);
        }
        if (arrow != null)
        {
            Destroy(arrow);
        }
    }
}
