 using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
[Serializable] 
public class RarityPercentageData
{
    [SerializeField]
    private string name;
    public string Name => name;
    [SerializeField]
    private float percentage;
    public float Percentage => percentage;
}
