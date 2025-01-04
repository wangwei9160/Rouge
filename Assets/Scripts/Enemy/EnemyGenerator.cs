using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class EnemyGenerator : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject monster;

    private float mCurrentSecond = 0;
    private int waveCount = 0;
    private bool isSet = false;

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

    private void Awake()
    {
        init();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        init();
    }

    private void init()
    {
        mCurrentSecond = 0;
        waveCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.state == StateID.FightState)
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
        }

        
    }


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

    }
}
