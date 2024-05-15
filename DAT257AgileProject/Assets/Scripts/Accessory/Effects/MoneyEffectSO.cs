using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyEffect", menuName = "Effects/Money Effect")]
public class MoneyEffectSO : EffectSO
{
    public override void ApplyEffect()
    {
        // TODO: rework money effect, the game build will not work using methods from the UnityEditor namespace
        string folderPath = "Assets/Resources/ScriptableObjects/Trash";

        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(folderPath);

        foreach (Object asset in assets)
        {
            if (asset != null && asset is TrashData)
            {
                TrashData trashData = (TrashData)asset;

                // The effect
                //trashData.MoneyValue *= 2;

                // dont know what this is lol
                EditorUtility.SetDirty(asset);
            }
        }
    }

    public override void UnApplyEffect()
    {

    }
}
