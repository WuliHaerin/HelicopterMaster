/*
\n










*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    public AudioSource
        menuTheme,
        mapTheme,
        click,
        gameplayTheme,
        playerExplode,
        chopGoingLoop,
        enemyShootSinger,
        enemyShootSpread,
        enemyShootRocket,
        rescue,
        getCoin,
        explodeTower,
        enemySmallExplode,
        enemyMediumExplode,
        shootHitEnemy,
        victory,
        levelMapSelect,
        needToUnlock,
        nextStep,
        hangarUpdate,
        takeDamage;

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
    void Start()
    {
        if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
        {
            menuTheme.Play();
        }
    }
}
