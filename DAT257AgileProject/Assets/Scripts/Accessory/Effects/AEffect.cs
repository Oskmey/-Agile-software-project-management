using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable] 
public abstract class AEffect
{
    [SerializeField]
    private EffectType effectType;
    public EffectType Effect => effectType;

    public abstract void ApplyEffect();}

public static class AEffectExtensions
{
    public static void ApplyEffect(){
        //TODO
    }

}



