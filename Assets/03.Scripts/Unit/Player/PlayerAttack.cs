using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Playables;

public class PlayerAttack : CharacterBase
{
    private Transform enemy;
    private float timer;

    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float impactOffeset = 1;
    [SerializeField]
    private PlayableDirector lastAttack;

    private int hashAttack = Animator.StringToHash("Attack");
    private PlayerStat playerStat;
    private PlayerSkill playerSkill;

    private CameraZoom cameraZoom;
    private ShockyTrigger shockyTrigger;

    private bool flagAction = false;

    private int difX;
    private int difY;

    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
        playerSkill = GetComponent<PlayerSkill>();
        enemy = Define.EnemyTrans;
        cameraZoom = Define.CameraTrans.GetComponent<CameraZoom>();
        shockyTrigger = Define.CameraTrans.GetComponent<ShockyTrigger>();

        EventManager.StartListening("STOPACTION", StopAction);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !flagAction)
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

        difX = MapController.PosToArray(enemy.localPosition.x) - MapController.PosToArray(transform.localPosition.x);
        difY = MapController.PosToArray(enemy.localPosition.z) - MapController.PosToArray(transform.localPosition.z);
        Debug.Log($"difx:{difX}, difY:{difY}");
        bool nearEnemy = (Mathf.Abs(difX) + Mathf.Abs(difY)) == 1 ? true : false;
        Vector2Int pos = MapController.PosToArray(transform.localPosition);
        int damage = MapController.Instance.MapNum[pos.y, pos.x];

        // Last Attack 연출
        if (nearEnemy && Define.EnemyStat.HP - damage <= 0)
        {
            EventManager.TriggerEvent("STOPACTION", new EventParam());
            lastAttack.Play();
            return;
        }

        PlayAnimator(hashAttack);
        if (Define.IsBossAlive == false)
            return; 
        Debug.Log("Attack");
        if (nearEnemy)
        {
            //playerStat.SetCombo(damage);
            playerSkill.StackDice(damage);
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

    private void StopAction(EventParam eventParam)
    {
        flagAction = true;
    }

    public void AttackAnimation()
    {
        // HP를 0으로 초기화
        Define.EnemyStat.GetDamage(Define.EnemyStat.HP);

        if (Mathf.Abs(difX) == 1)
        {
            if (enemy.transform.localPosition.x > transform.localPosition.x)
                shockyTrigger.ChangePos(0.55f, 0.2f);
            else if (enemy.transform.localPosition.x < transform.localPosition.x)
                shockyTrigger.ChangePos(0.45f, 0.2f);
        }
        else if(Mathf.Abs(difY) == 1)
        {
            if (enemy.transform.localPosition.z > transform.localPosition.z)
                shockyTrigger.ChangePos(0.5f, 0.3f);
            else if (enemy.transform.localPosition.z < transform.localPosition.z)
                shockyTrigger.ChangePos(0.5f, 0.1f);
        }

        PlayAnimator(hashAttack);

        GameObject particle = ObjectPool.Instance.GetObject(PoolObjectType.AttackParticle);
        particle.transform.position = new Vector3(enemy.localPosition.x, enemy.localPosition.y + impactOffeset, enemy.localPosition.z);

        Define.CameraTrans.DOShakePosition(0.7f, 0.1f);

        Time.timeScale = 0.4f;
        Invoke("OrginTime", 0.8f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("STOPACTION", StopAction);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("STOPACTION", StopAction);
    }
}
