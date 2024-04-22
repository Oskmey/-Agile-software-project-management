using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasableItem
{
    // Define sprites for each Type
    public static Dictionary<Type, Sprite> ItemSprites = new Dictionary<Type, Sprite>();

    static PurchasableItem()
    {
        // Loads the sprite to each Type
        ItemSprites[Type.Egg1] = Resources.Load<Sprite>("PurchasableItemSprites/Egg1");
        ItemSprites[Type.Egg2] = Resources.Load<Sprite>("PurchasableItemSprites/Egg2");

    }
    public enum Type
    {
        Egg1,
        Egg2
    }

    public static int GetCost(Type type)
    {
        switch (type)
        {
            case Type.Egg1:     return 100;
            case Type.Egg2:     return 200;
            default: 
                Debug.LogError("Cost not found for PurchasableItem.Type: " + type); 
                return 0;
        }
    }

    public static Sprite GetSprite(Type type)
    {
        if (ItemSprites.ContainsKey(type))
        {
            return ItemSprites[type];
        }
        else
        {
            Debug.LogError("Sprite not found for PurchasableItem.Type: " + type);
            return null;
        }
    }
}
