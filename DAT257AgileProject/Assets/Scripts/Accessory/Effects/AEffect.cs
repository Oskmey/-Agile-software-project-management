using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using UnityEngine;
using System;


[Serializable] 
public class AEffect
{
    [SerializeField]
    private Effect effectType;
    public Effect Effect => effectType;

}

public static class AEffectExtensions

{
    public static void ApplyEffect(){
        //TODO
    }

}



