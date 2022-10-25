using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ParticleSystem))]
public class Item : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Transform enemy;

    public int damage;

    public bool isAttack = false;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
       particleSystem = GetComponent<ParticleSystem>();
       enemy = Define.EnemyTrans;
    }

    private void Update()
    {
        if (isAttack) return;
        if (particleSystem.time >= 0.4f)
            isAttack = true;
        if(particleSystem.IsAlive())
        {
            if(MapController.PosToArray(enemy.localPosition) == MapController.PosToArray(transform.localPosition))
            {
                isAttack = true;
                Define.PlayerTrans.GetComponent<PlayerAttack>().ClearDamage();
                Define.EnemyStat.GetDamage(damage);
                if (Define.EnemyStat.HP <= 0)
                {
                    EventManager.TriggerEvent("STOPACTION", new EventParam());
                    GameManager.Instance.LoadStageScene(2);
                }
                Define.CameraTrans.DOShakePosition(0.7f, 0.1f);
                transform.DOScale(1.5f, 0.2f);
                ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage).GetComponent<NumText>().DamageText(damage, Define.EnemyStat.transform.position);
                Time.timeScale = 0.1f;
                Invoke("OriginTime", 0.06f);
            }
        }
    }

    private void OriginTime()
    {
        Time.timeScale = 1;
    }
}
