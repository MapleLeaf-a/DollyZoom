using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public Transform target; //摄像机要移动到的目标

    public float startDis = 10f;
    public float endDis = 2f;

    public float startFOV = 60f; //初始视场角

    public float duration = 3f; //运动时间

    private Camera cam; //摄像机
    private float timer = 0f; //当前时间
    private Vector3 direction; //摄像机到目标的方向

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = startFOV;

        direction = (target.position - transform.position).normalized; //计算摄像机到目标的方向
        transform.position = target.position - direction * startDis; //设置摄像机初始位置
    }

    
    void Update()
    {
        if (timer >= duration) return;

        timer += Time.deltaTime;
        float t = timer / duration; //算出插值百分比

        float distance = Mathf.SmoothStep(startDis, endDis, t); //平滑插值计算当前距离

        transform.position = target.position - direction * distance; //设置摄像机位置

        //保持主体大小不变，计算摄像机视场角
        float fov = 2.0f * Mathf.Atan(
                startDis / distance * Mathf.Tan(startFOV * 0.5f * Mathf.Deg2Rad)
            ) * Mathf.Rad2Deg;

        //fov = Mathf.Clamp(fov, 20f, 100f);

        cam.fieldOfView = fov; //设置摄像机视场角
    }
}
