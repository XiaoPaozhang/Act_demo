using UnityEditor;
using UnityEngine;

public class ScriptingDefineSymbolsExample
{
  [MenuItem("Tools/Check Scripting Define Symbols")]
  public static void CheckScriptingDefineSymbols()
  {
    BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone;
    string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
    Debug.Log($"Current scripting define symbols for {buildTargetGroup}: {defineSymbols}");
  }
}
