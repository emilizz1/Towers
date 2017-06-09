using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

    PanelManager panelManager;

    RectTransform rectPanelManager;
    RectTransform rectField;

    public Vector3 offset;

    Pip[] pips;
    
	void Start () {
        panelManager = GetComponentInParent<PanelManager>();
        rectPanelManager = panelManager.transform as RectTransform;
        pips = GetComponentsInChildren<Pip>();
        rectField = gameObject.transform as RectTransform;
        offset = rectPanelManager.position - rectField.position;
	}
	
	public void SelectField()
    {
        foreach(Pip pip in pips)
        {
            pip.transform.position += offset;
        }
    }
}
