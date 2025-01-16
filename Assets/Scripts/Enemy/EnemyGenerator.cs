using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class EnemyGenerator : ManagerBaseWithoutPersist<EnemyGenerator>
{

    public GameObject monster;

    private float mCurrentSecond = 0;
    private int waveCount = 0;

    private int[,] Mons = new int[,]
    {
        {0 , 0 , 0 , 1 , 0 , 0 , 0 },
        {0 , 0 , 1 , 0 , 0 , 0 , 0 },
        {0 , 0 , 0 , 0 , 1 , 0 , 0 },
        {0 , 0 , 0 , 0 , 0 , 1 , 0 },
        {0 , 0 , 0 , 0 , 0 , 0 , 1 },
        {0 , 1 , 0 , 0 , 0 , 0 , 0 },
        {0 , 0 , 0 , 1 , 0 , 0 , 0 }
    };

    private List<GameObject> monsterController = new List<GameObject>();

    void Start()
    {
        init();
    }

    private void init()
    {
        mCurrentSecond = 0;
        waveCount = 0;
    }

    void Update()
    {
        if(GameManager.Instance.state == StateID.FightState)
        {
            mCurrentSecond += Time.deltaTime;

            if (mCurrentSecond > 1)
            {
                mCurrentSecond = 0;

                if (waveCount <= 6)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (Mons[waveCount, i] == 1)
                        {
                            GameObject tmp = GameObject.Instantiate(monster, gameObject.transform);
                            tmp.transform.position = new Vector3(9 - i * 3, 0, -15);
                            monsterController.Add(tmp);
                        }

                    }

                    waveCount = waveCount + 1;
                }
            }

            List<GameObject> toRemove = new List<GameObject>();

            foreach (var item in monsterController)
            {
                MonsterController tmp = item.GetComponent<MonsterController>();
                if (tmp.Status == 5)
                {
                    StartCoroutine(DieAction(item));
                    tmp.ChangeStatus(6);
                    GameContext.number.res += 1;
                    EventCenter.Broadcast(EventDefine.RefreshEnemyCount);
                    GameManager.Instance.gameData.GetExp(10);
                    toRemove.Add(item);
                }
            }

            foreach (var item in toRemove)
            {
                monsterController.Remove(item);
            }
        }
        else
        {
            init();
            foreach (var item in monsterController)
            {
                Destroy(item);
            }
        }

    }

    // 获得最前方的敌人
    public GameObject GetEnemy()
    {
        if(monsterController.Count == 0)
        {
            return null;
        }
        GameObject tmp = monsterController.First();
        return tmp;
    }


    public IEnumerator DieAction(GameObject monster)
    {
        yield return new WaitForSeconds(1f);
        monster.GetComponent<Animator>().enabled = false;
        monster.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        monster.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        monster.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        monster.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        monster.SetActive(false);
        Destroy(monster);
    }

}
