/*
\n










*/

using System.Collections;
using UnityEngine;

public class PlayerController : Gun
{
    public bool isRevive;
    private float startPoint = 0.0f;

    private int
        HP = 100,
        originalHP = 100;

    private bool isTouching = false;

    public static PlayerController Instance { get; private set; }

    [Space(10)]
    public Rigidbody2D rigid2D;
    public float flyForce = 2.0f;

    [Space(10)]
    [Tooltip("The distance from the left side of the screen")]
    public float extraDistance = 0.0f;

    [Space(10)]
    public GameObject helicoper;
    public Drones
        drones1,
        drones2;

    [Space(10)]
    public int maxUpgradedArmorLvl = 1;
    public int
        maxUpgradedGunLvl = 1,
        maxUpgradedDronesLvl = 1;

    [Space(5)]
    public GameObject helicopter1;
    public GameObject
        helicopter2,
        helicopter3,
        upgradedMainGun,
        normalSideGun,
        upgradedSideGun;

    [Space(5)]
    public GameObject notifyRescued;

    public bool Dead { get; set; }

    // Behaviour messages
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Behaviour messages
    new void Start()
    {
        base.Start();

        SetstartPosition();
        SetUp();
    }

    private void SetstartPosition()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        startPoint = worldPoint.x + extraDistance;
    }

    public void Revive()
    {
        AdManager.ShowVideoAd("dcf3ceb4l0t27kdek8",
            (bol) => {
                if (bol)
                {
                    HP = originalHP;
                    UIManager.Instance.SetPlayerHP(HP, originalHP);
                    isRevive = true;
                    StopCoroutine("GameFalse");
                    UIManager.Instance.SetRevive(false);

                    AdManager.clickid = "";
                    AdManager.getClickid();
                    AdManager.apiSend("game_addiction", AdManager.clickid);
                    AdManager.apiSend("lt_roi", AdManager.clickid);
                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });

    }

    // Behaviour messages
    new void Update()
    {
        base.Update();

        if (isTouching)
        {
            //rigid2D.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
            rigid2D.velocity = Vector2.up * flyForce;
        }
    }

    public void PointerDown()
    {
        isTouching = true;

        if (!GameController.Instance.StartFire)
        {
            GameController.Instance.StartFire = true;
            GameController.Instance.StartLevel();

            if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
            {
                SoundManager.Instance.gameplayTheme.Play();
            }

            rigid2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    public void PointerUp()
    {
        isTouching = false;
    }

    // Behaviour messages
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Const.COIN_TAG)
        {
            if (PlayerPrefs.GetInt(Const.SOUND, 1) == 1)
            {
                SoundManager.Instance.getCoin.Play();
            }

            GameController.Instance.CreateEffect(
                    Const.COIN_EFFECT,
                    GameController.Instance.effectCoinPrefab,
                    GameController.Instance.effectCoinHolder,
                    col.transform.position);

            col.gameObject.SetActive(false);

            GameController.Instance.UpdateCoin();
        }

        if (col.tag == Const.ENEMY_TAG || col.tag == Const.ENEMY_CAR_TAG || col.tag == Const.BELVEDERE_TAG)
        {
            if (!GameController.Instance.GameOver)
            {
                if (PlayerPrefs.GetInt(Const.SOUND, 1) == 1)
                {
                    if (col.tag == Const.BELVEDERE_TAG)
                    {
                        SoundManager.Instance.explodeTower.Play();
                    }
                    else
                    {
                        SoundManager.Instance.enemySmallExplode.Play();
                    }
                }

                GameController.Instance.KilledEnemy++;
                GameController.Instance.EnemyBonus += col.GetComponent<EnemySoldier>().bonus;

                GameController.Instance.CreateEffect(
                        Const.EXPLOSION_EFFECT,
                        GameController.Instance.explosionPrefab,
                        GameController.Instance.explosionHolder,
                        col.transform.position);

                col.gameObject.SetActive(false);

                HP -= 10;
                UIManager.Instance.SetPlayerHP(HP, originalHP);
                if (HP <= 0)
                {
                    UIManager.Instance.SetRevive(true);
                    StartCoroutine("GameFalse");
                }
            }
        }

        if (col.tag == Const.ENEMY_BULLET_TAG)
        {
            if (PlayerPrefs.GetInt(Const.SOUND, 1) == 1)
            {
                SoundManager.Instance.takeDamage.Play();
            }

            if (!GameController.Instance.Win && !GameController.Instance.GameOver)
            {
                HP -= 10;
                UIManager.Instance.SetPlayerHP(HP, originalHP);
                if (HP <= 0)
                {
                    UIManager.Instance.SetRevive(true);
                    StartCoroutine("GameFalse");
                }
            }

            col.gameObject.SetActive(false);
        }

        if (col.tag == Const.PARACHUTIST_TAG)
        {
            if (PlayerPrefs.GetInt(Const.SOUND, 1) == 1)
            {
                SoundManager.Instance.rescue.Play();
            }

            notifyRescued.SetActive(true);
            GameController.Instance.Rescue();
            col.gameObject.SetActive(false);
        }
    }

    private IEnumerator GameFalse()
    {
        yield return new WaitForSeconds(0.1f);
        GameController.Instance.CreateEffect(
                        Const.EXPLOSION_EFFECT,
                        GameController.Instance.explosionPrefab,
                        GameController.Instance.explosionHolder,
                        transform.position);

        helicoper.SetActive(false);
        ActiveOrInActiveDrones(false);
        StartCoroutine("Die");
    }

    private IEnumerator Die()
    {
        if (PlayerPrefs.GetInt(Const.SOUND, 1) == 1)
        {
            SoundManager.Instance.playerExplode.Play();
            SoundManager.Instance.chopGoingLoop.Stop();
        }

        if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
        {
            SoundManager.Instance.gameplayTheme.Stop();
        }

        Dead = true;
        StopCoroutine("Shoot");
        GameController.Instance.StopAllCoroutines();
        rigid2D.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(1.5f);

        GameController.Instance.GameOver = true;
        UIManager.Instance.GameOver();
    }

    public void ActiveOrInActiveDrones(bool value)
    {
        drones1.gameObject.SetActive(value);
        drones2.gameObject.SetActive(value);
    }

    public void SetUp()
    {
        SetUpHelicopter();

        transform.position = new Vector3(startPoint, 0.0f, 0.0f);

        //
        originalHP = HP = 100 + (PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) * 15);
        UIManager.Instance.SetPlayerHP(HP, originalHP);

        Dead = false;
        isTouching = false;
        activateGun = false;

        if (bulletList != null)
        {
            for (var i = bulletList.Count - 1; i >= 0; i--)
            {
                Destroy(bulletList[i]);
            }
            bulletList.Clear();
        }

        drones1.Reset();
        drones2.Reset();
        GameController.Instance.Reset();
    }

    private void SetUpHelicopter()
    {
        helicoper.SetActive(true);

        if (PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) >= 3 && PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) < 6)
        {
            helicopter1.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) > 6 && PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) < 10)
        {
            helicopter1.SetActive(false);
            helicopter2.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(Const.ARMOR_LEVEL, 1) >= 10)
        {
            helicopter2.SetActive(false);
            helicopter3.SetActive(true);
        }

        if (PlayerPrefs.GetInt(Const.DRONES_LEVEL, 0) >= 1)
        {
            drones1.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(Const.DRONES_LEVEL, 0) == maxUpgradedDronesLvl)
        {
            drones2.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(Const.PLAYER_GUN_LEVEL, 1) >= 8)
        {
            upgradedMainGun.SetActive(true);
        }
        else
        {
            upgradedMainGun.SetActive(false);
        }

        if (PlayerPrefs.GetInt(Const.PLAYER_GUN_LEVEL, 1) >= 2 && PlayerPrefs.GetInt(Const.PLAYER_GUN_LEVEL, 1) <= 6)
        {
            normalSideGun.SetActive(true);
        }
        else
        {
            normalSideGun.SetActive(false);
        }

        if (PlayerPrefs.GetInt(Const.PLAYER_GUN_LEVEL, 1) > 6)
        {
            upgradedSideGun.SetActive(true);
        }
        else
        {
            upgradedSideGun.SetActive(false);
        }
    }
}
