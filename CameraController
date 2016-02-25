using UnityEngine;
using System.Collections;
/// <summary>
/// 摄像机控制
/// by:何天宇
/// 2015.12.28
/// </summary>
public class CameraController : MonoBehaviour {

    public float movSpeed;

    private float m_X;
    private float m_Y;

	void LateUpdate () {
        Move();
        Rotate();
	}

    /// <summary>
    /// 移动摄像机
    /// </summary>
    private void Move() {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward*Time.deltaTime*movSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= this.transform.right* Time.deltaTime * movSpeed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= this.transform.forward* Time.deltaTime * movSpeed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * Time.deltaTime * movSpeed;
        }
    }

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    private void Rotate() {
        if (Input.GetMouseButton(1))
        {
            m_X += Input.GetAxis("Mouse X");
            m_Y -= Input.GetAxis("Mouse Y");

            this.transform.rotation=Quaternion.Euler(m_Y,m_X,0);
        }
    }
}
