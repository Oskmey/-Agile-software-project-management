using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RecyclingTests
{
    private RecyclingMachine recyclingMachine;
    private static readonly TrashData[] trashData = Resources.LoadAll<TrashData>("ScriptableObjects");

    [SetUp]
    public void Setup()
    {
        GameObject recyclingMachineGameObject = new GameObject();
        recyclingMachineGameObject.AddComponent<RecyclingMachine>();
        recyclingMachine = recyclingMachineGameObject.GetComponent<RecyclingMachine>();
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(recyclingMachine);
    }
}
