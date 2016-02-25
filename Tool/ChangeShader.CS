using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 高亮显示模型
/// by:何天宇
/// 2016.1.4
/// </summary>
public class ChangeShader : MonoBehaviour {
    //高亮shader
    public Shader outLineShader;
    //默认shader
    public Shader defaultShader;
    //材质
    private List<Material> m_Mat;

    void Awake() {
        GetMaterials();
    }

    /// <summary>
    /// 鼠标进入替换成高亮shader
    /// </summary>
    void OnMouseEnter()
    {
        SetShader(outLineShader);
    }

    /// <summary>
    /// 鼠标移出替换成默认shader
    /// </summary>
    void OnMouseExit()
    {
        SetShader(defaultShader);
    }

    /// <summary>
    /// 获得所有材质
    /// </summary>
    private void GetMaterials() { 

        m_Mat = new List<Material>();

        Renderer temp = this.GetComponent<Renderer>();
        if (temp!=null)
        {
            for (int j = 0; j < temp.materials.Length; j++)
            {
                m_Mat.Add(temp.materials[j]);
            }    
        }

        Renderer[] mesh = this.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < mesh.Length; i++)
        {
            for (int j = 0; j < mesh[i].materials.Length; j++)
            {
                m_Mat.Add(mesh[i].materials[j]);
            }
        }
       
    }

    /// <summary>
    /// 设置材质shader
    /// </summary>
    /// <param name="sh">shader</param>
    private void SetShader(Shader sh) {
        for (int i = 0; i < m_Mat.Count; i++)
        {
            m_Mat[i].shader = sh;
        }
    }
}
