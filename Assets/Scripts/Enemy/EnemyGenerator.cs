using System.Collections;
using UnityEngine;


public class EnemyGenerator : Singleton<EnemyGenerator>
{

    public GameObject[] monster;

    private float mCurrentSecond = 0;
    private int waveCount = 0;
    private int waveKill = 0;

    private WaveTplInfo waveInfo => GameManager.Instance.WaveDic[GameManager.Instance.gameData.CurrentWave];

    //private List<GameObject> monsterController = new List<GameObject>();

    private void OnEnable()
    {
        //Debug.Log("AddListener EventDefine.OneEnemyDeath");
        EventCenter.AddListener<GameObject>(EventDefine.OneEnemyDeath, EnemyDie);
    }
    private void OnDisable()
    {
        //Debug.Log("RemoveListener EventDefine.OneEnemyDeath");
        EventCenter.RemoveListener<GameObject>(EventDefine.OneEnemyDeath, EnemyDie);
    }

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

                if (waveCount < waveInfo.EnemyWave)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if(waveInfo.EnemyIDs[waveCount][i] == 0)  continue;
                        if (waveInfo.EnemyIDs[waveCount][i] != 0)
                        {
                            GameObject tmp = GameObject.Instantiate(monster[waveInfo.EnemyIDs[waveCount][i] - 1], gameObject.transform);
                            tmp.transform.position = new Vector3(9 - i * 3, 0, -15);
                            //monsterController.Add(tmp);
                        }
                    }

                    waveCount = waveCount + 1;
                }
            }

            //List<GameObject> toRemove = new List<GameObject>();

            //foreach (var item in monsterController)
            //{
            //    MonsterController tmp = item.GetComponent<MonsterController>();
            //    if (tmp.Status == 5)
            //    {
            //        StartCoroutine(DieAction(item));
            //        tmp.ChangeStatus(6);
            //        GameContext.CurrentWaveKill += 1;
            //        GameManager.Instance.gameData.GetExp(10);
            //        toRemove.Add(item);
            //    }
            //}

            //foreach (var item in toRemove)
            //{
            //    monsterController.Remove(item);
            //}
            //EventCenter.Broadcast(EventDefine.RefreshEnemyCount);
        }
        else
        {
            init();
        }
    }

    // 获得一个敌方目标
    public GameObject GetEnemy()
    {
        foreach (Transform item in gameObject.transform)
        {
            GameObject tmp = item.gameObject;
            //if(tmp.GetComponent<MonsterController>().Status < 5)
            //{
            //    return tmp;
            //}
            if(tmp.GetComponent<BaseEnemy>().Status < 5)
            {
                return tmp;
            }
        }
        return null;
    }

    /// <summary>
    /// 怪物死亡效果，修改动画，添加击杀数，获取经验值
    /// </summary>
    /// <param name="monster">死亡的怪物</param>
    public void EnemyDie(GameObject monster)
    {
        StartCoroutine(DieAction(monster));
        GameContext.CurrentWaveKill += 1;
        //GameManager.Instance.gameData.GetExp(monster.GetComponent<MonsterController>().Exp);
        GameManager.Instance.gameData.GetExp((int)monster.GetComponent<BaseEnemy>().attr.经验值);
        EventCenter.Broadcast(EventDefine.RefreshEnemyCount) ;
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
