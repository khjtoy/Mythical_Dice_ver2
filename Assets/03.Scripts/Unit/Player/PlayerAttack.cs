using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class PlayerAttack : CharacterBase
{
    [SerializeField]
    private AudioSource attackSource;
    [FormerlySerializedAs("playerAttackSounds")] [SerializeField]
    private SoundSO playerAttackSoundsSo;

    private enum AttackSounds { Slash, SlashFail}

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

    private Vector3 itemPos;
    private bool isItem = false;
    private bool isAction = false;
    private bool isSkill = false;
    GameObject itemObject;

    private List<Item> items = new List<Item>();

    protected override void Start()
    {
        base.Start();
        playerStat = GetComponent<PlayerStat>();
        playerSkill = GetComponent<PlayerSkill>();
        enemy = Define.EnemyTrans;
        cameraZoom = Define.CameraTrans.GetComponent<CameraZoom>();
        shockyTrigger = Define.CameraTrans.GetComponent<ShockyTrigger>();

        EventManager.StartListening("STOPACTION", StopAction);
        EventManager.StartListening("PLAYACTION", PlayAction);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !flagAction && !isAction)
        {
            playerStat.PlayerMove.moveDir.Clear();
            CheckPos();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (isItem)
        {
            if (MapController.PosToArray(transform.position) == MapController.PosToArray(itemPos))
            {
                itemObject.SetActive(false);
                isSkill = true;
                isItem = false;
            }
        }
    }

    private void CheckPos()
    {
        if (enemy.transform.localPosition.x > transform.localPosition.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (enemy.transform.localPosition.x < transform.localPosition.x)
            transform.localScale = new Vector3(-1, 1, 1);

        if (isSkill)
            StartCoroutine(Skill(playerStat.PlayerMove.PlayerDir));
        else
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

        if (damage == 0) return;
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
            SoundManager.Instance.AudioChange(playerAttackSoundsSo.audioClips[(int)AttackSounds.Slash], attackSource);
            // 파티클 생성
            Define.EnemyStat.GetDamage(damage);
            ObjectPool.Instance.GetObject(PoolObjectType.PopUpDamage).GetComponent<NumText>().DamageText(damage, Define.EnemyStat.transform.position);
            GameObject particle = ObjectPool.Instance.GetObject(PoolObjectType.AttackParticle);
            particle.transform.position = new Vector3(enemy.localPosition.x, enemy.localPosition.y + impactOffeset, enemy.localPosition.z);
            Define.CameraTrans.DOShakePosition(0.7f, 0.1f);

            timer = attackDelay;
        }
        else
            SoundManager.Instance.AudioChange(playerAttackSoundsSo.audioClips[(int)AttackSounds.SlashFail], attackSource);
    }

    private IEnumerator Skill(int dir)
    {
        items.Clear();

        Vector2Int pos = MapController.PosToArray(transform.localPosition);
        int damage = MapController.Instance.MapNum[pos.y, pos.x];
        isAction = true;
        if(dir == 0)
        {
            for(int i = pos.x + 1; i < GameManager.Instance.Size; i++)
            {
                SkillAction(i, pos.y, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if (dir == 1)
        {
            for (int i = pos.x - 1; i >= 0; i--)
            {
                SkillAction(i, pos.y, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if (dir == 2)
        {
            for (int i = pos.y + 1; i < GameManager.Instance.Size; i++)
            {
                SkillAction(pos.x, i, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }
        else if (dir == 3)
        {
            for (int i = pos.y - 1; i >= 0; i--)
            {
                SkillAction(pos.x, i, damage);
                yield return new WaitForSeconds(0.2f);
            }
        }

        isSkill = false;
        isAction = false; 
    }

    private void SkillAction(int x, int z, int damage)
    {
        Vector3 skillPos = MapController.ArrayToPos(x, z);
        //skillPos.z += 1;
        GameObject effect = ObjectPool.Instance.GetObject(PoolObjectType.SkillParticle);
        effect.transform.localPosition = new Vector3(skillPos.x, 1, skillPos.z);
        effect.GetComponent<Item>().damage = damage * 2;
        items.Add(effect.GetComponent<Item>());

    }

    public void ClearDamage()
    {
        foreach(Item item in items)
        {
            item.isAttack = true;
        }
    }

    private void OrginTime()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
    }

    private void StopAction(EventParam eventParam)
    {
        flagAction = true;
    }
    private void PlayAction(EventParam eventParam)
    {
        flagAction = false;
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
        DOTween.timeScale = 0.4f;
        Invoke("OrginTime", 0.8f);
    }
    public void SpawnItem(Vector3 pos)
    {
        if (isItem) return;

        itemPos = pos;
        isItem = true;

        itemObject = ObjectPool.Instance.GetObject(PoolObjectType.ItemParticle, false);
        itemObject.transform.localPosition = new Vector3(itemPos.x, 1.2f, itemPos.z - 0.5f); 
        itemObject.SetActive(true);

    }

private void OnDestroy()
    {
        EventManager.StopListening("STOPACTION", StopAction);
        EventManager.StopListening("PLAYACTION", PlayAction);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("STOPACTION", StopAction);
        EventManager.StopListening("PLAYACTION", PlayAction);
    }
}
