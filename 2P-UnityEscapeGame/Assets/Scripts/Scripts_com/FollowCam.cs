using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // 고정값 (카메라랑 플레이어 사이의 거리)

    void Update()
    {
        transform.position = target.position+ offset;
    }
}
