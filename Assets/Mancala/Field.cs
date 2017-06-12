using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

    PanelManager panelManager;
    Button button;
    Pip[] pips;

    RectTransform rectPanelManager;
    RectTransform rectField;

    public Vector3 offset;
    public bool fieldIsMovable = true;
    
	void Start () {
        panelManager = GetComponentInParent<PanelManager>();
        button = GetComponent<Button>();
        pips = GetComponentsInChildren<Pip>();

        rectPanelManager = panelManager.transform as RectTransform;
        rectField = gameObject.transform as RectTransform;

        offset = rectPanelManager.position - rectField.position;
        CheckForColorsAtStart();
    }

    public void PlacableField()
    {
        button.colors = ColorChange(Color.yellow, Color.red);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(gameObject.transform, true);
        PipCount();
        pips[pips.Length -1].ChangedParentField();
    }

    private void PipCount()
    {
        pips = GetComponentsInChildren<Pip>();
    }
}
