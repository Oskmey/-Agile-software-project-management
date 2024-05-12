using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDataHandler : MonoBehaviour
{
    private FileDataHandler<VolumeData> volumeFileDataHandler;

    public VolumeData LoadData()
    {
        return volumeFileDataHandler.Load();
    }
}
