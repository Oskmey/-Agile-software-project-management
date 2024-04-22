using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    private Transform shopItmeTemplate;
    void Awake()
    {
        container = transform.Find("Container");
        shopItmeTemplate = container.Find("ShopItemTemplate");
        shopItmeTemplate.gameObject.SetActive(false);
    }

}
