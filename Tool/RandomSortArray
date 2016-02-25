    /// <summary>
    /// 对数组进行随机排列
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    public static void RandomSortArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int num = Random.Range(i, array.Length);
            T temp = array[num];
            array[num] = array[i];
            array[i] = temp;
        }
    }
