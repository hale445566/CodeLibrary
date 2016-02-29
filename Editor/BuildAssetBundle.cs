using UnityEditor;
/// <summary>
/// 插件 打包assetbundle
/// </summary>
public class BuildAssetBundle : Editor{

    [MenuItem("MyTool/BuildAssetBundle")]
    public static void Build() {
        BuildPipeline.BuildAssetBundles("打包的路径");
    }

}

