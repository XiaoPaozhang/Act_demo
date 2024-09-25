
namespace Act_demo
{
  /// <summary>
  /// 资源加载工具方法类
  /// </summary>
  public static class AssetUtility
  {
    // 提取公共路径作为常量
    private const string GAME_ASSET_PATH = "Assets/Act_demo/";

    // 重写方法，使用模板字符串和常量
    public static string GetConfigAsset(string assetName, bool fromBytes)
    {
      return $"{GAME_ASSET_PATH}Configs/{assetName}.{(fromBytes ? "bytes" : "txt")}";
    }

    public static string GetLubanTableAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}LubanTables/{assetName}.json";
    }

    //本地化语言文本资源
    public static string GetDictionaryAsset(string assetName, bool fromBytes)
    {
      return $"{GAME_ASSET_PATH}Localization/{GameEntry.Localization.Language}/Dictionaries/{assetName}.{(fromBytes ? "bytes" : "xml")}";
    }

    public static string GetFontAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}Fonts/{assetName}.ttf";
    }

    public static string GetSceneAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}Scenes/{assetName}.unity";
    }

    public static string GetMusicAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}Music/{assetName}.mp3";
    }

    public static string GetSoundAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}Sounds/{assetName}.wav";
    }

    public static string GetEntityAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}Entities/{assetName}.prefab";
    }

    //UI资源
    public static string GetUIFormAsset(string uiFormName)
    {
      return $"{GAME_ASSET_PATH}UI/UIForms/{uiFormName}.prefab";
    }

    public static string GetUIItemsAsset(UIItemsType uIItemsType)
    {
      return $"{GAME_ASSET_PATH}UI/UIItems/{uIItemsType}.prefab";
    }

    public static string GetUISoundAsset(string assetName)
    {
      return $"{GAME_ASSET_PATH}UI/UISounds/{assetName}.wav";
    }
  }
}