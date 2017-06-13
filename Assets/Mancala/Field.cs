using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

    //TODO create socket for pips to fall

    PanelManager panelManager;
    Button button;
    Pip[] pips;

    RectTransform rectPanelManager;
    RectTransform rectField;

    public Vector3 offset;
    public bool fieldIsMovable = true;

    private bool placable = false;
    private int pipsMustBePlaced;
    
	void Start () {
        panelManager = GetComponentInParent<PanelManager>();
        button = GetComponent<Button>();
        pips = GetComponentsInChildren<Pip>();

        rectPanelManager = panelManager.transform as RectTransform;
        rectField = gameObject.transform as RectTransform;

        offset = rectPanelManager.position - rectField.position;
        CheckForColorsAtStart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (placable)
        {
            collision.transform.SetParent(gameObject.transform, true);

            RecountPips();
            pips[pips.Length - 1].ChangedParentField();

            pipsMustBePlaced--;
            if (pipsMustBePlaced <= 0)
            {
                placable = false;
                CheckForColorsAtStart();
                
            }
        }
    }

    public int PipCount()
    {
        pips = GetComponentsInChildren<Pip>();
        return pips.Length;
    }


    public void PlacableField()
    {
        button.colors = ColorChange(Color.yellow, Color.yellow);
        pipsMustBePlaced++;
        placable = true;
    }

    private void SelectField()
    {
        if (fieldIsMovable)
        {
            foreach (Pip pip in pips)
            {
                pip.transform.position += offset;
                pip.pipIsMovable = true;
            }
            panelManager.FieldSelected(this, pips.Length);
        }
    }

    public void CheckForColorsAtStart()
    {
        pipsMustBePlaced = 0;
        if (fieldIsMovable)
        {
            button.colors = ColorChange(Color.green, Color.green);
        }
        else
        {
            button.colors = ColorChange(Color.red, Color.red);
        }
    }

    private ColorBlock ColorChange(Color colorNormal, Color colorElse)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = colorNormal;
        colorBlock.highlightedColor = colorElse;
        colorBlock.pressedColor = colorElse;
        return colorBlock;
    }



    public void StopPipMovement()
    {
        foreach(Pip pip in pips)
        {
            pip.pipIsMovable = false;
        }
    }

    private void RecountPips()
    {
        pips = GetComponentsInChildren<Pip>();
    }
}
