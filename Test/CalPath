    /// <summary>
    /// 计算弧线 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    Vector3[] CalPath(Transform start, Transform end)
    {
        //向量中点
        Vector3 half = (start.position + end.position) * 0.5f;
        //中点上移
        Vector3 normalPos=half+Vector3.up;
        //计算方向向量 垂直于中点和向量所组成的平面
        Vector3 normal= Vector3.Cross(start.position - normalPos, end.position - normalPos).normalized;
        //计算垂点 垂点越像下弧度越大 垂点越向上弧度越小
        Vector3 center = normal * (len) + half;

        Debug.DrawLine(start.position, center, Color.red);
        Debug.DrawLine(end.position, center, Color.red);

        Vector3 s = start.position - center;
        Vector3 e = end.position - center;

        //长度为关键点*精度+关键点的数量
        Vector3[] path = new Vector3[precision];
        for (int i = 0; i < precision; i++)
        {
            path[i] = Vector3.Slerp(s, e, (float)i / (float)precision)+center;
        }

        Debug.DrawLine(s+center, path[0], Color.red);
        for (int j = 0; j < path.Length - 1; j++)
        {
            Debug.DrawLine(path[j], path[j + 1], Color.red);
        }
        Debug.DrawLine(path[path.Length - 1], e+center, Color.red);

        //path[10] = end.position;
        return path;
    }
