using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
  public void destroying() {
    // 动画播放完成后调用
    Destroy(gameObject);
  }
}
