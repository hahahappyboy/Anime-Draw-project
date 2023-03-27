/// <summary>
/// 参数配置
/// </summary>
public static class Config {
    //Lua文件路ing
    public const string LUA_PATH = "/Resources/Lua/";
    
    //本地档案Json数据地址
    public const string INSTANCEZONEINFO_JSON_PATH = "Data/InstanceZone";
    //颜色信息的Json数据
    public const string COLORCHOOSEINFO_JSON_PATH = "Data/ColorChoose";
    
    # region Prefabs
    //档案Prefab地址
    public const string ZONE_LAYOUT_PREFAB_PATH = "Prefab/Zone Layout";
    //颜色选择Prefab地址
    public const string COLOR_CHOOSE_ITEM_PREFAB_PATH = "Prefab/ColorChooseItem";
    //角色选择prefab地址
    public const string CHARACTER_CHOOSE_ITEM_PREFAB_PATH = "Prefab/CharacterChooseItem";
    //MaskPrefab地址
    public const string MASK_CHOOSE_ITEM_PREFAB_PATH = "Prefab/MaskChooseItem";
    # endregion
    
    //HTTP请求地址
    public const string POST_HTTP_URL = DOWNLOAD_HTTP_URL+"AnimeDraw";
    //图像下载地址
    public const string DOWNLOAD_HTTP_URL = "http://192.168.42.130:5000/";
    
    # region 五等分
    //角色信息的Json数据
    public const string CHARACTERCHOOSEINFO_FIVE_JSON_PATH ="Data/Five/CharacterChoose_Five";
    //Mask信息的Json数据
    public const string MASKCHOOSEINFO_FIVE_JSON_PATH ="Data/Five/MaskChoose_Five";
    //模型ID
    public const string MODEL_ID_FIVE = "five";
    # endregion
    
    # region 国家队
    //角色信息的Json数据
    public const string CHARACTERCHOOSEINFO_DARLING_JSON_PATH ="Data/Darling/CharacterChoose_Darling";
    //Mask信息的Json数据
    public const string MASKCHOOSEINFO_DARLING_JSON_PATH ="Data/Darling/MaskChoose_Darling";
    //模型ID
    public const string MODEL_ID_DARLING = "darling";
    # endregion
    
    
    

}
