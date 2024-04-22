using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Flags]public enum FishingSpotRarities

{

    
    Common = 1,
    Uncommon = 2, //| Common,
    Rare = 4 ,//| Common,
    Epic = 8, //| Common,
    Legendary = 16, //| Common,

    DoNotUseEverything = 32

}
