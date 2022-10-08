using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform enemy;
    private float timer;

    [SerializeField]
    UnitAnimation animation;
    [SerializeField]
    private float attackDelay;

    private int hashAttack = Animator.StringToHash("Attack");
    private void Start()
    {
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
        int difY = MapController.PosToArray(enemy.localPosition.y) - MapController.PosToArray(transform.localPosition.y);
        float add = Mathf.Abs(difX) + Mathf.Abs(difY);

        animation.PlayAnimator(hashAttack);
        Debug.Log("Attack");
        if (add == 1)
        {
            Debug.Log("АјАн");
            timer = attackDelay;
        }
    }
}
