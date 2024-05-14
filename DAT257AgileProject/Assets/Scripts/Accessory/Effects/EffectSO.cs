using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectSO : ScriptableObject
{
    [SerializeField]
    private EffectType effectType;
    public EffectType Effect => effectType;

    public abstract void ApplyEffect();

}
public enum EffectType
{
    Speed,
    Money,
    Luck
}
