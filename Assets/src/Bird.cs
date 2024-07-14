using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
  private bool isClicked = false; // 是否处于点击状态
  public float maxDis = 3;

  private SpringJoint2D sp;

  private Rigidbody2D rg;

  // 均开放为 public, 直接在场景中绑定即可
  public LineRenderer rightLine;
  public LineRenderer leftLine;
  public Transform rightPos;
  public Transform leftPos;

  // 场景激活钩子
  private void Awake() {
    // ? 调用 GetComponent API 均用于获取挂载到物体上的组件
    // 获取2D组件(挂载到物体上的内容)
    sp = GetComponent<SpringJoint2D>();
    // 获取 Rigidbody2D(刚体)组件
    rg = GetComponent<Rigidbody2D>();
  }

  // 鼠标按下时调用
  private void OnMouseDown() {
    isClicked = true;
    // 鼠标点下时开启动力学计算
    rg.isKinematic = true;
  }

  // 处理小鸟飞出
  private void Fly() {
    // 延迟弹簧组件失活时间, 用于计算弹射
    sp.enabled = false; // 禁用弹簧组件(SpringJoint2D)
  }

  // 鼠标抬起时调用
  private void OnMouseUp() {
    isClicked = false;
    // 松手时停止物理计算
    rg.isKinematic = false;
    Invoke("Fly", 0.1f);
    // 线条消失
  }

  // 场景刷新时调用
  private void Update() {
    // 实时监测鼠标状态
    if (isClicked) {
      // 鼠标处于按下状态, 设置鼠标跟随
      // ? 需要转换坐标系, 场景坐标以左下角为原点(0, 0), 但是世界坐标系是以相机为原点(0, 0)
      // * 通过 Camera.main.ScreenToWorldPoint 将输入参数转换为世界坐标系(默认3d, 2d游戏需要处理Z轴问题)
      // * 处理Z轴, 1. 直接设置Z轴默认数值 2. 减去摄像机的z轴位置
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // transform.position += new Vector3(0, 0, 10);
      transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

      // 位置限定
      if (Vector3.Distance(transform.position, rightPos.position) > maxDis) {
        Vector3 pos = (transform.position - rightPos.position).normalized; // 单位化向量
        pos *= maxDis; // 最大长度向量
        transform.position = pos + rightPos.position;
      }

      // 划线
      Line();
    }
  }


  // 划线
  private void Line() {
    // 右边, 划线
    rightLine.SetPosition(0, rightPos.position);
    rightLine.SetPosition(1, transform.position);

    leftLine.SetPosition(0, leftPos.position);
    leftLine.SetPosition(1, transform.position);
  }
}