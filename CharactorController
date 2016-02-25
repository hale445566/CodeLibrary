using UnityEngine;
using System.Collections;
/// <summary>
/// 角色控制器
/// by:何天宇
/// 2015.12.31
/// </summary>
public class CharactorController : MonoBehaviour {
    private float move_H;               //水平轴移动插值
    private float move_V;               //垂直轴移动插值

    private int moveSpeed;              //移动速度

    private float mouse_X;              //鼠标水平轴移动插值和
    private float mouse_Y;              //鼠标垂直轴移动插值和

    private float rotateSpeed;          //镜头旋转速度

    private float distance;              //镜头角色距离

    private Transform cameraTransform;  //摄像机transform
    private Camera m_Camera;            //摄像机

    private Ray groundRay;              //射向地面的射线
    private RaycastHit groundHit;       //射线碰撞

    private int scaleSpeed;             //镜头拉近速度
    private Vector3 cameraTargetOffset; //镜头焦点插值

    private int fieldOfViewMin=10;      //最小视野
    private int fieldOfViewMax=60;      //最大视野

    private Animator animator;          //动画控制器
	void Awake() {
        InitComponent();
	}

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitComponent (){
        cameraTransform = Camera.main.transform;
        m_Camera = Camera.main;
        animator = this.GetComponent<Animator>();

        groundRay = new Ray(this.transform.position+Vector3.up, Vector3.down);
    }

    void Start() {
        InitValue();
        
        SetCamera();
    }

    /// <summary>
    /// 初始化数值
    /// </summary>
    private void InitValue() {
        distance = 3;
        scaleSpeed = 20;
        moveSpeed = 2;
        rotateSpeed = 80;
        cameraTargetOffset = new Vector3(0, 2, 0);
        mouse_X = -90f;
        mouse_Y = 2.5f;
    }

    //模型操作
    void Update()
    {
        //移动模型
        MoveCharactor();

        //确保模型站在地上
        SetGround();
    }

    //摄像机操作
    void LateUpdate() {
        if (Input.GetMouseButton(1))
            RotateCamera();

        SetCamera();

        ScaleCamera();
    }

    /// <summary>
    /// 模型站在地上
    /// </summary>
    private void SetGround() {
        groundRay = new Ray(this.transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(groundRay,out groundHit,100f,~Global.playerLayer))
        {
            //根据模型轴点不同修改方法
            this.transform.position = new Vector3(transform.position.x, groundHit.point.y, transform.position.z);
        }   
    }

    /// <summary>
    /// 移动角色
    /// </summary>
    private void MoveCharactor()
    {
        move_V = Input.GetAxis("Vertical");
        move_H = Input.GetAxis("Horizontal");


        if (move_H != 0 || move_V != 0)
        {
            Vector3 temp = new Vector3(move_H,0,move_V);
            //向量长度检测 避免Look rotation viewing vector is zero错误
            if ((cameraTransform.rotation * temp.normalized).sqrMagnitude>0.001f)
            {
                //以摄像机坐标系对水平面投影后的坐标系为参考坐标系进行正前方校正
                this.transform.forward = Vector3.ProjectOnPlane(cameraTransform.rotation * temp.normalized, Vector3.up);
                //移动
                this.transform.localPosition += cameraTransform.rotation * temp.normalized * Time.deltaTime * moveSpeed;
                //设置动画
                animator.SetFloat(Global.ani_MoveSpeed,temp.sqrMagnitude);
            }
            
        }
    }

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    private void RotateCamera()
    {
        mouse_X += Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
        mouse_Y -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

    }

    /// <summary>
    /// 设置摄像机位移旋转
    /// </summary>
    private void SetCamera() {
        Quaternion q = Quaternion.Euler(mouse_Y, mouse_X, 0);

        cameraTransform.position = q * new Vector3(0, 0, -distance) + this.transform.position + cameraTargetOffset;
        cameraTransform.rotation = q;
    }

    /// <summary>
    /// 缩放摄像机
    /// </summary>
    private void ScaleCamera() {
       float temp= Input.GetAxis("Mouse ScrollWheel");
       if (temp != 0)
       {
           m_Camera.fieldOfView -= temp * scaleSpeed;
          m_Camera.fieldOfView= Mathf.Clamp(m_Camera.fieldOfView, fieldOfViewMin, fieldOfViewMax);
       }
    }

}
