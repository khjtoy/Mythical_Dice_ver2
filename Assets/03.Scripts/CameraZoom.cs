using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms;

public class CameraZoom : MonoBehaviour
{
    public bool ZoomActive;

    public Vector3 originPos;
    public float originSize;
    public Transform targetPos;
    public float targerSize;

    public Camera cam;

    public float moveSpeed = 1f;
    public float sizeSpeed = 0.7f;

    private float inverseMoveTime;

    private void Start()
    {
        cam = Camera.main;
        originPos = cam.transform.position;
        originSize = cam.orthographicSize;
    }

    private bool fire = false;

    private bool outFire = false;

    public bool ZoomTriger
    {
        set { fire = value; }  
    }

    public bool OutTriger
    {
        set { outFire = value; }
    }

    public void LateUpdate()
    {
        if(fire)
        {
            cam.DOOrthoSize(targerSize, sizeSpeed);
            transform.DOLocalMove(new Vector3(targetPos.localPosition.x, targetPos.localPosition.y + 3, targetPos.localPosition.z), moveSpeed).OnComplete(() =>
            {
                Define.IsUsingSkill = false;
                fire = false;
            });
        }

        if(outFire)
        {
            cam.DOOrthoSize(originSize, sizeSpeed);
            transform.DOLocalMove(originPos, moveSpeed).OnComplete(() =>
            {
                outFire = false;
            });
        }
    }

}
