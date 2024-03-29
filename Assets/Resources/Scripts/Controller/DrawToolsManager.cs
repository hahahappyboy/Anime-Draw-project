using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawToolsManager : BaseMonoBehaviour {
        
    private Button penButton;
    private Button brushButton;
    private Button undoButton;
    private Button saveButton;
    private Button defaultButton;
    private Slider penSlider;

    private Color originalButtonColor;
    private Color pressButtonColor = new Color(0, 0, 245/255f, 128/255f);

    protected override void FetchComponent() {
        penButton = transform.Find("Pen Button").GetComponent<Button>();
        originalButtonColor = penButton.GetComponent<Image>().color;
        
        
        brushButton = transform.Find("Brush Button").GetComponent<Button>();
        undoButton = transform.Find("Undo Button").GetComponent<Button>();
        saveButton = transform.Find("Save Button").GetComponent<Button>();
        defaultButton = transform.Find("Default Button").GetComponent<Button>();
        penSlider = transform.Find("Pen Slider").GetComponent<Slider>();
        
        penButton.onClick.AddListener(ButtonListenerPenButton);
        brushButton.onClick.AddListener(ButtonListenerBrushButton);
        undoButton.onClick.AddListener(ButtonListenerUndoButton);
        saveButton.onClick.AddListener(ButtonListenerSaveButton);
        defaultButton.onClick.AddListener(ButtonListenerDefaultButton);
        penSlider.onValueChanged.AddListener(ButtonListenerSlider);
        penSlider.value = 3;
    }

   
    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        //默认笔画被选中
        ButtonListenerPenButton();
    }
    
    
    # region 监听
    private void ButtonListenerPenButton() {
        if (MaskDrawManager.instance==null) {
            return;
        }
        
        //把自己的变深，其他的变为原来颜色
        penButton.GetComponent<Image>().color = pressButtonColor;
        brushButton.GetComponent<Image>().color = originalButtonColor;
        MaskDrawManager.instance.SetToolType(PenType.Pen);
    }
    private void ButtonListenerBrushButton() {
        if (MaskDrawManager.instance==null) {
            return;
        }
        brushButton.GetComponent<Image>().color = pressButtonColor;
        penButton.GetComponent<Image>().color = originalButtonColor;
        MaskDrawManager.instance.SetToolType(PenType.Brush);
    }
    private void ButtonListenerSaveButton() {
        
    }
    private void ButtonListenerUndoButton() {
        if (MaskDrawManager.instance==null) {
            return;
        }
        MaskDrawManager.instance.Undo();
    }
    private void ButtonListenerDefaultButton() {
        if (MaskDrawManager.instance==null) {
            return;
        }
        MaskDrawManager.instance.Default();
    }
    
    private void ButtonListenerSlider(float value) {
        if (MaskDrawManager.instance==null) {
            return;
        }
        MaskDrawManager.instance.SetPenWidth((int)value);
    }
    #endregion
    

}
