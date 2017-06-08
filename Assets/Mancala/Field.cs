using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

    PanelManager panelManager;

	// Use this for initialization
	void Start () {
        panelManager = GetComponentInParent<PanelManager>();
	}
	
	public void SelectField()
    {
        gameObject.transform.position = panelManager.transform.position;
    }
}
