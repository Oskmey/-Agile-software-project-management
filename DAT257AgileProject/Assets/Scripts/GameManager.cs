using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] private TextMeshProUGUI recycledTrashCountText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + playerStatsManager.Money.ToString();
    }

    void UpdateRecycledTrashCountText()
    {
        recycledTrashCountText.text = "Recycling Count: " + playerStatsManager.TrashRecycledList.Count.ToString();
    }
}
