using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {

    PanelManager panelManager;

    public void Selected()
    {
        if (FindObjectOfType<PanelManager>())
        {
            panelManager = FindObjectOfType<PanelManager>();
            panelManager.StartingOver();
        }
        else
        {
            Debug.Log("Cant use this button");
        }
    }
}
