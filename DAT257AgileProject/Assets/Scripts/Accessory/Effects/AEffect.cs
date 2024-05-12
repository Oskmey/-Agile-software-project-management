using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Speed,
    Money, 
    Luck
}

public abstract class AEffect : MonoBehaviour
{
    [SerializeField]
    private EffectType effectType;
    public EffectType EffectType => effectType;
    public abstract void ApplyEffect();
}
