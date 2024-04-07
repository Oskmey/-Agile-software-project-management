using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
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

    public void startMinigame()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);

        tutorialText.GetComponent<TextMeshProUGUI>().text = "press spacebar to catch";
    }
}
