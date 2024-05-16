using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AccessorySO", menuName = "ScriptableObjects/AccessoryScriptableObject")]
public class AccessorySO : ScriptableObject
{
    [Header("Accessory Settings")]
    [Tooltip("Name of the item")]
    [SerializeField]
    private string accessoryName;
    public string AccessoryName { get => accessoryName; }
    [Tooltip("Price of the item")]
    public int cost;
    [Tooltip("Sprite of the item")]
    public Sprite sprite;

    [Header("Accessory Rarity")]
    [Tooltip("Rarity of the item")]
    public AccessoryRarity rarity;

    [Header("Accessory effects")]
    [Tooltip("Effects the items has on the player")]
    [SerializeReference]
    [SerializeField]
    public List<AEffect> accessoryEffects;
}