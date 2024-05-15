using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyEffect", menuName = "Effects/Money Effect")]
public class MoneyEffectSO : EffectSO
{
    [SerializeField]
    float moneyMult;
    public override void ApplyEffect()
    {
        GameObject recycling = GameObject.FindGameObjectWithTag("Recycling Manager");
        RecyclingManager manager = recycling.GetComponent<RecyclingManager>();
        if(manager.MoneyMultiplier > 1)
        {
            manager.MoneyMultiplier = 1;
        }
        else
        {
            manager.MoneyMultiplier = moneyMult;
        }


        //string folderPath = "Assets/Resources/ScriptableObjects/Trash";

        //Object[] assets = AssetDatabase.LoadAllAssetsAtPath(folderPath);

        //foreach (Object asset in assets)
        //{
        //    if (asset != null && asset is TrashData)
        //    {
        //        TrashData trashData = (TrashData)asset;

        //        // The effect
        //        trashData.moneyValue = (int)(trashData.moneyValue * (1 + moneyMult));

        //        // dont know what this is lol
        //        EditorUtility.SetDirty(asset);
        //    }
        //}
    }

    public override void UnApplyEffect()
    {

    }
}
