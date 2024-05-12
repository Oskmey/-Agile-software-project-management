using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    [Header ("Money stats")]
    [SerializeField]
    private TextMeshProUGUI currentMoneyText;
    [SerializeField]
    private TextMeshProUGUI totalMoneyGainedText;
    [SerializeField]
    private TextMeshProUGUI totalMoneySpentText;
    [SerializeField]
    private TextMeshProUGUI accessoriesPurchasedText;

    [Header("Inventory stats")]
    [SerializeField]
    private TextMeshProUGUI trashCaughtText;
    [SerializeField]
    private TextMeshProUGUI currentInventoryItemsText;
    [SerializeField]
    private TextMeshProUGUI currentlyEquippedAccessoriesText;
    [SerializeField]
    private TextMeshProUGUI recycledTrashText;

}
