using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using UnityEngine;
using UnityEngine.UI;

public class MaskDrawManager : BaseMonoBehaviour
{
    # region 组件
    //mask图像组件
    public Sprite maskSprite;
    //mask的纹理
    private Texture2D maskTexture2D;
    
    # endregion
    # region 属性

    
    
    //当前选中的工具类型
    private PenType _currentPenType;
    //单例
    public static MaskDrawManager instance;
    //笔的颜色
    public  Color32 penColour;
    //笔的宽度
    public  int penWidth;
    //之前画的位置，默认为0，0
    private Vector2 previousDragPosition = Vector2.zero;
    //存放最初的颜色数组
    private Color32[] orignalColorArray;
    //存放目前的颜色数组
    private Color32[] currentColorArray;
    //存放画的前一张图，用于撤回
    private Color32[] previousColorArray;
    //图像的宽高
    private int spriteHeight;
    private int spriteWidth;
    //当前是否在滑动
    private bool isDraging = false;
    //保存画过的点
    private Stack<Color32[]> savePixelStack;
    //画笔类型，是涂鸦还是一键填充
    private PenType penType = PenType.Pen;
    # endregion
    
    # region 生命周期
    protected override void FetchComponent() {
        instance = this;
        savePixelStack = new Stack<Color32[]>();
        
        maskSprite = this.GetComponent<Image>().sprite;
        
        
        maskTexture2D = maskSprite.texture;
        orignalColorArray = maskTexture2D.GetPixels32();
        //当前sprite各个像素的颜色
        currentColorArray = maskTexture2D.GetPixels32();
        spriteHeight = (int)maskSprite.rect.height;
        spriteWidth = (int)maskSprite.rect.width;
    }
    
    protected override void GetResources() {
        
    }

    protected override void InitZoneLayout() {
        
    }

    private void Update() {
        Debug.Log(penType);
    }

    private void OnDestroy() {
        //退出的时候，恢复为最初的图像
        maskTexture2D.SetPixels32(orignalColorArray);
        maskTexture2D.Apply();
    }

    # endregion
    /// <summary>
    /// 设置mask的图像
    /// </summary>
    /// <param name="color32"></param>
    public void SetTexture2D(Color32[] color32) {
        currentColorArray = color32;
        maskTexture2D.SetPixels32(currentColorArray);
        maskTexture2D.Apply();
    }
    /// <summary>
    /// 设置ToolType的类型
    /// </summary>
    /// <param name="type"></param>
    public void SetToolType(PenType type) {
        switch (type) {
            case PenType.Pen:
                penType = PenType.Pen;
                break;
            case PenType.Brush:
                penType = PenType.Brush;
                break;
            default:
                break;
        }
        
    }
    
    
   
}
