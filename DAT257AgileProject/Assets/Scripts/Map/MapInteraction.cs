using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapInteraction : Ainteractable
{
    public override string text => "Press E to open map";
    private GameObject ui;
    public override void interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        if (ui != null)
        {
            ui.SetActive(true);
            getPlayerMaps();
        }
    }

    public void Start()
    {
        PlayerExitHandler += playerExitEvent;
    }

    public void Update()
    {
        if (Time.timeScale <= 0 && ui != null)
        {
            ui.SetActive(false);
        }
    }

    
    private void playerExitEvent()
    {
        ui.SetActive(false);
    }

    private void getPlayerMaps()
    {
        // Get player maps
    }

}
