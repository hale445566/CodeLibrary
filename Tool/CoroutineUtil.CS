using UnityEngine;
using System.Collections;

/// <summary>
/// 协程工具类，为非MonoBehaviour类提供启动协程方法
/// </summary>
public class CoroutineUtil : MonoBehaviour
{
    private static CoroutineUtil instance;

    public static CoroutineUtil Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("_CoroutineUtil").AddComponent<CoroutineUtil>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>
    /// 开启协程
    /// </summary>
    public static Coroutine StartMonoCoroutine(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    /// <summary>
    /// 真实时间等待函数，用于代替受timeScale影响的WaitForSeconds
    /// </summary>
    /// <param name="seconds">等待秒数</param>
    public static Coroutine WaitForRealSeconds(float seconds)
    {
        return Instance.StartCoroutine(WaitRealSeconds(seconds));
    }

    private static IEnumerator WaitRealSeconds(float seconds)
    {
        float startSecond = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startSecond + seconds)
        {
            yield return null;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
