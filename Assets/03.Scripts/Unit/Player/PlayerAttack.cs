using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : CharacterBase
{
    private Transform enemy;
    private float timer;

    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float impactOffeset = 1;

    private int hashAttack = Animator.StringToHash("Attack");
    private PlayerStat playerStat;


    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        enemy = Define.EnemyTrans;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            CheckPos();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void CheckPos()
    {
        if (enemy.transform.localPosition.x > transform.localPosition.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (enemy.transform.localPosition.x < transform.localPosition.x)
            transform.localScale = new Vector3(-1, 1, 1);

        AttackAction(MapController.PosToArray(transform.localPosition.x), MapController.PosToArray(transform.localPosition.y));
    }

    private void AttackAction(int x, int y)
    {
        if (timer > 0) return;

        int difX = MapController.PosToArray(enemy.localPosition.x) - MapController.PosToArray(transform.localPosition.x);
        int difY = MapController.PosToArray(enemy.localPosition.z) - MapController.PosToArray(transform.localPosition.z);
        Debug.Log($"difx:{difX}, difY:{difY}");
        bool nearEnemy = (Mathf.Abs(difX) + Mathf.Abs(difY)) == 1 ? true : false;
        
        PlayAnimator(hashAttack);
        if (Define.IsBossAlive == false)
            return; 
        Debug.Log("Attack");
        if (nearEnemy)
        {
            Vector2Int pos = MapController.PosToArray(transform.localPosition);
            int damage = MapController.Instance.MapNum[pos.y, pos.x];
            playerStat.SetCombo(damage);
            bool FlagCombo = playerStat.COMBO >= 20;
            // 파티클 생성
            
            Define.EnemyStat.GetDamage(damage);
            ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage).GetComponent<NumText>().DamageText(damage, Define.EnemyStat.transform.position);
            GameObject particle = ObjectPool.Instance.GetObject(FlagCombo ? PoolObjectType.ComboParticle : PoolObjectType.AttackParticle);
            particle.transform.position = new Vector3(enemy.localPosition.x, enemy.localPosition.y + impactOffeset, enemy.localPosition.z);
            
            Define.CameraTrans.DOShakePosition(0.7f, 0.1f);
            if(FlagCombo)
            {
                Time.timeScale = 0.4f;
                Invoke("OrginTime", 0.12f);
            }

            timer = attackDelay;
        }
    }

    private void OrginTime()
    {
        Time.timeScale = 1;
    }
}
