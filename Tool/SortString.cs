/// <summary>
    /// 对string进行排序 按字母由大到小的顺序排列
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string BubbleSort(string s)
    {
        StringBuilder sb = new StringBuilder(s);
        for (int i = 0; i < sb.Length-1; i++)
        {
            for (int j = i+1; j < sb.Length; j++)
            {
                if (sb[i]> sb[j])
                {
                    char temp = sb[i];
                    sb[i] = sb[j];
                    sb[j] = temp;
                }
            }
        }

        return sb.ToString();
    }