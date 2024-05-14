using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using TMPro;
using UnityEngine;

public class MapInteraction : Ainteractable
{
    public override string text => "Press E to open map";

    public override void interact()
    {
        GameObject ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        if (ui != null)
        {
            ui.SetActive(true);
            getPlayerMaps();
        }
    }

    private void getPlayerMaps()
    {
        // Get player maps
    }

}
