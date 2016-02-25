using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 播放记录的动作
/// by:何天宇
/// 2016.2.25
/// </summary>
public class PlayRecord : MonoBehaviour
{
    private static PlayRecord instance;
    private Queue<RecordTransform> record = new Queue<RecordTransform>();
    private float offSet=0.3f;

    public static PlayRecord Instance()
    {
        if (instance == null)
            instance = new GameObject("PlayRecord").AddComponent<PlayRecord>();

        return instance;
    }

    public void Play(Queue<RecordTransform> record)
    {
        this.record = record;
        StartCoroutine(PlayNow());
    }

    IEnumerator PlayNow()
    {
        while (true)
        {
            if (record.Count == 0)
                break;

            //while循环 用户同一时间输入移动和旋转
            while (Mathf.Abs(Time.time - record.Peek().time) < offSet)
            {
                RecordTransform rt = record.Dequeue();
                RecordCommond_Type r = rt.GetRecordCommondType();

                switch (r)
                {
                    case RecordCommond_Type.Position:
                        this.transform.position = rt.GetValue();
                        break;
                    case RecordCommond_Type.Rotation:
                        this.transform.eulerAngles = rt.GetValue();
                        break;
                    default:
                        break;
                }

                if (record.Count == 0)
                    break;
            }

            yield return 0;
        }
    }
}