using System;
using System.Collections.Generic;
using UnityEngine;

// The existence of this class is to provide a way to access information
// about trash and connect it to the sources using a dictionary. Because
// dictionaries are not serializable, the dictionary could not be in a
// scriptable object. This class is a workaround to that problem.
public class TrashInfoHandler : MonoBehaviour
{
    [SerializeField]
    private List<SourceData> sources;
    public static List<SourceData> Sources { get; private set; }

    private static readonly IReadOnlyDictionary<string, SourceData> trashBagInfo = CreateTrashBagInfoDictionary();

    private void Awake()
    {
        Sources = sources;
    }

    public static IReadOnlyDictionary<string, SourceData> GetInfoDictionary(TrashType trashType)
    {
        switch (trashType)
        {
            case TrashType.TrashBag:
                return trashBagInfo;
            default:
                throw new ArgumentException("Invalid trash type", nameof(trashType));
        }
    }

    private static IReadOnlyDictionary<string, SourceData> CreateTrashBagInfoDictionary()
    {
        Dictionary<string, SourceData> trashBagInfo = new()
        {
            {"half of all plastic ever produced was just in the last 20 years?", Sources[0] },
            {"around 8 million tons of plastic waste gets into the ocean yearly from coastal nations?", Sources[0]},
            {"additives are often used in plastics and can prolong the lifespan of the plastic to various degrees with some estimates being at least 400 years for the material to break down?", Sources[0] },
            {"most of the trash in the ocean was carried from land, where for instance rivers can act as a transport method for the trash", Sources[0] },
            {"trash in the ocean breaks down into small particles called microplastics due to sunlight, wind, and wave actions?", Sources[0] },
            {"plastics have killed millions of animals all from birds to marine creatures?", Sources[0] },
            {"close to 700 species of animals have been affected by plastic, where most seabirds have eaten plastic?", Sources[0] },
            {"most animal deaths from plastics are from starvation or entanglement?", Sources[0] },
            {"plastics can cause harm to livers and cells as well as disrupting the reproduction system of some species?", Sources[0] },
            {"a solution to stopping plastic pollution could be better waste management systems and recycling?", Sources[0] },
            {"a solution to stopping plastic pollution could be better package designs which accounts for the limited life of the product?", Sources[0] },
            {"a solution to stopping plastic pollution could be reducing the manufacturing of unneeded single-use plastics?", Sources[0] }
        };

        return trashBagInfo;
    }
}