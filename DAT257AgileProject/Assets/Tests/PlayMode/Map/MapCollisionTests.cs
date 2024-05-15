using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class MapCollision : InputTestFixture
{
    private GameObject player;
    private GameObject map;
    private GameObject managers;

    private string sceneName = "TestCollisionScene"; 
    private Keyboard keyboard;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        SceneManager.LoadScene("TestCollisionScene");
        keyboard = InputSystem.AddDevice<Keyboard>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        map = GameObject.FindObjectOfType<Grid>().gameObject;
        managers = GameObject.FindObjectOfType<PlayerStatsManager>().gameObject;
    }

    private bool IsCollisionWithMap()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.GetComponent<BoxCollider2D>().bounds.center, player.GetComponent<BoxCollider2D>().bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Map"))
            {
                return true;
            }
        }
        return false;
    }

   [UnityTest]
    public IEnumerator MapCollisionWithTopWaterTile()
    {
        Press(keyboard.wKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.wKey);

        Assert.IsTrue(collisionResult, "Player did not collide with top water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }

    [UnityTest]
    public IEnumerator MapCollisionWithLeftWaterTile()
    {
        Press(keyboard.aKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.aKey);

        Assert.IsTrue(collisionResult, "Player did not collide with left water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }

    [UnityTest]
    public IEnumerator MapCollisionWithRightWaterTile()
    {
        Press(keyboard.dKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.dKey);

        Assert.IsTrue(collisionResult, "Player did not collide with right water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }

    [UnityTest]
    public IEnumerator MapCollisionWithBottomWaterTile()
    {
        Press(keyboard.sKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.sKey);

        Assert.IsTrue(collisionResult, "Player did not collide with bottom water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }

    // S needs to come first for some reason
    [UnityTest]
    public IEnumerator MapCollisionWithBottomRightWaterTile()
    {
        Press(keyboard.sKey);
        Press(keyboard.dKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.dKey);
        Release(keyboard.sKey);

        Assert.IsTrue(collisionResult, "Player did not collide with bottom right water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }
    
    // S needs to come first for some reason
    [UnityTest]
    public IEnumerator MapCollisionWithBottomLeftWaterTile()
    {
        Press(keyboard.sKey);
        Press(keyboard.aKey);
        yield return new WaitForSeconds(1);
        bool collisionResult = IsCollisionWithMap();
        Release(keyboard.aKey);
        Release(keyboard.sKey);

        Assert.IsTrue(collisionResult, "Player did not collide with bottom left water tile. Please consider looking into the sprite editor to ensure the tile is set up correctly.");
    }


    [TearDown]
    public override void TearDown() 
    {
        base.TearDown();
        foreach (var gameObject in Object.FindObjectsOfType<GameObject>()) 
        {
            Object.Destroy(gameObject);
        }
    }
}