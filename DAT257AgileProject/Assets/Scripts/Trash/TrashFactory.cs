using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFactory : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> trashPrefabs;
    public static List<GameObject> TrashPrefabs { get; private set; }

    private void Awake()
    {
        TrashPrefabs = trashPrefabs;
    }
    
    public static GameObject CreateTrash(TrashType trashType)
    {
        GameObject trashPrefab = TrashPrefabs.Find(prefab => prefab.GetComponent<TrashScript>().TrashType == trashType);
        if (trashPrefab == null)
        {
            Debug.LogError($"No prefab found for TrashType: {trashType}");
            return null;
        } 
        else
        {
            return Instantiate(trashPrefab);
        }
    }

    public static GameObject CreateRandomTrashBasedOnRarity(TrashRarity trashRarity)
    {
        IReadOnlyList<GameObject> trashPrefabsWithRarity = TrashPrefabs.FindAll(prefab => prefab.GetComponent<TrashScript>().Rarity == trashRarity);
        if (trashPrefabsWithRarity.Count == 0)
        {
            Debug.LogError($"No prefab found for TrashRarity: {trashRarity}");
            return null;
        }
        return Instantiate(trashPrefabsWithRarity[Random.Range(0, trashPrefabsWithRarity.Count)]);
    }
}
