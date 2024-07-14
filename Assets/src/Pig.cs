using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
  // 碰撞检测方法（有相互作用力, 实现物理碰撞效果）

  // 大于最大速度, 猪才会死亡, 最大最小之间则受伤
  public float maxSpeed = 10; // 最大速度
  public float minSpeed = 5; // 最小速度

  // 受伤时直接换图片, 获取sprit renderer组件
  private SpriteRenderer renderer;

  // 受伤图(public才会展示到U3d Script组件的属性上, 允许直接拖拽图片赋值)
  public Sprite hurtImgSrc;

  private void Awake() {
    // 获取小猪身上挂载的 SpriteRenderer 组件
    renderer = GetComponent<SpriteRenderer>();
  }

  // 碰撞测方法, 还有一个是 onTriggerEnter2D, 主要用于触发检测, 只需要保证其中一个物体挂载 RigidBody2D， 并且勾选上 is Trigger
  // 而OnCollisionEnter2D是碰撞检测, 需要物体上同时挂在 RigidBody2D和Circle Collider 2D(碰撞盒)
  private void OnCollisionEnter2D(Collision2D collision) {
    // ? 需要两个物体同时挂载 刚体(Rigid Body) 以及碰撞盒(Circle Collider)
    // ? 获取碰撞检测时的相对速度 collision.relativeVelocity
    // - 相对速度 collision.relativeVelocity 是一个向量, 需要获取其 magnitude, 才是其速度(无方向)
    // - 这个向量是一个 Vector2
    // - 再一个此时碰撞的速度应该是猪+小鸟的速度总和, 也就是相对速度
    var currentRelativeSpeed = collision.relativeVelocity.magnitude;
    print(currentRelativeSpeed);
    if (currentRelativeSpeed > maxSpeed) {
      // 死亡(直接移除模型即可)
      // TODO 缺少死亡动画播放
      Destroy(gameObject);
    } else if (currentRelativeSpeed > minSpeed && currentRelativeSpeed < maxSpeed) {
      // 受伤(更换图片到受伤图片)
      renderer.sprite = hurtImgSrc;
    } else {
      // 什么也不发生
    }
  }
}
