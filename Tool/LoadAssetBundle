    /// <summary>
    /// 从assetbundle中加载图片 
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!路径前要加file://!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="name"></param>
    public void LoadAssetBundle(string[] name,Action<Sprite[]> callBack)
    {
        StartCoroutine(Load(name,callBack));
    }

    IEnumerator Load(string[] name ,Action<Sprite[]> callBack)
    {
        WWW www = WWW.LoadFromCacheOrDownload(Global.questionImagePath + "/questionimage", 5);
        yield return www;

        AssetBundle ab = www.assetBundle;
        Sprite[] sprite = new Sprite[name.Length];
        for (int i = 0; i < name.Length; i++)
        {
            Texture2D obj = (Texture2D)ab.LoadAsset(name[i]);
            sprite[i] = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height), new Vector2(obj.width / 2, obj.height / 2));
            sprite[i].name = name[i];
        }

        //释放assetbundle资源
        ab.Unload(false);

        callBack(sprite);
    }
