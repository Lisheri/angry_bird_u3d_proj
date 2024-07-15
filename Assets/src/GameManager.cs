using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public List<Bird> birds;
  public List<Pig> pigs;

  public static GameManager _instance;

  // 记录小鸟初始化位置， 计算下一只时使用
  private Vector3 originBirdPos;
  private void Awake() {
    _instance = this;
    if (birds.Count > 0) {
      originBirdPos = birds[0].transform.position;
    };
  }

  private void Start() {
    // 初始化小鸟
    Initialize();
  }

  // 初始化小鸟
  public void Initialize() {
    // 初始化时仅第一只小鸟初始化
    for (int i = 0; i < birds.Count; i++) {
      var currentBird = birds[i];
      if (i == 0) {
        // 调整位置
        currentBird.transform.position = originBirdPos;
        currentBird.enabled = true; // 启用
        currentBird.sp.enabled = true; // 启用

      } else {
        currentBird.enabled = false; // 禁用
        currentBird.sp.enabled = false; // 禁用弹簧组件
      }
    }
  }


  public void NextBird() {
    // 如果没有赢, 则放出下一只小鸟, 若是赢了, 就不需要了
    if (pigs.Count > 0) {
      // 进一步判定小鸟
      if (birds.Count > 0) {
        // 下一只小鸟起飞
        Initialize();
      } else {
        // fail
      }
    } else {
      // success

    }
  }
}
