using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowBoxMinigame : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject tutorialText;

    GameObject arrow;
    GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void DestroyMinigame() // Runs on both "Success" and "Very Bad!"
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
