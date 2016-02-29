    /// <summary>
    /// 从assetbundle中加载图片 
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="name"></param>
    public void LoadAssetBundle(string[] name,System.Action<object[]> callBack)
    {
        StartCoroutine(Load(name,callBack));
    }

    IEnumerator Load(string[] name ,Action<Sprite[]> callBack)
    {
        WWW www = WWW.LoadFromCacheOrDownload("路径", 5);
        yield return www;

        AssetBundle ab = www.assetBundle;
        object[] objArray=new object[name.Length];
        for (int i = 0; i < name.Length; i++)
        {
            obj[i] = (Texture2D)ab.LoadAsset(name[i]);
        }
        //释放assetbundle资源
        ab.Unload(false);

        callBack(obj);
    }
