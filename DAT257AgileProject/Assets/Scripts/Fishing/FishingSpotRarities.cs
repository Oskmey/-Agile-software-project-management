using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Flags]public enum FishingSpotRarities

{

    
    Common = 1,
    Uncommon = 2,
    Rare = 4,
    Epic = 8,
    Legendary = 16,

    DoNotUseEverything = 32

}
