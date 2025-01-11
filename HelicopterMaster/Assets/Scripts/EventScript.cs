/*
\n










*/

using UnityEngine;

public class EventScript : MonoBehaviour
{

    private ParticleSystem ps;

    // Behaviour messages
    void Awake()
    {
        if (tag == Const.EXPLOSION_TAG)
            ps = GetComponent<ParticleSystem>();
    }

    // Behaviour messages
    void Update()
    {
        if (tag == Const.EXPLOSION_TAG)
        {
            if (ps.isStopped)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Animation Event for coin effect or parachutist
    public void Disapper()
    {
        gameObject.SetActive(false);
    }

    // Animation Event for fade screen
    public void FadeScreen()
    {
        if (UIManager.Instance.ToHome)
        {
            if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
            {
                SoundManager.Instance.mapTheme.Stop();
                SoundManager.Instance.menuTheme.Play();
            }

            UIManager.Instance.ToHome = false;
            UIManager.Instance.SwitchToHome();
        }

        if (UIManager.Instance.ToOptions)
        {
            UIManager.Instance.ToOptions = false;
            UIManager.Instance.SwitchToOptions();
        }

        if (UIManager.Instance.ToTutorials)
        {
            UIManager.Instance.ToTutorials = false;
            UIManager.Instance.SwitchToTutorials();
        }

        if (UIManager.Instance.ToGame)
        {
            if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
            {
                SoundManager.Instance.mapTheme.Stop();
                SoundManager.Instance.menuTheme.Stop();
            }

            UIManager.Instance.ToGame = false;
            UIManager.Instance.SwitchToGame();
        }

        if (UIManager.Instance.ToMap)
        {
            Time.timeScale = 1.0f;

            if (UIManager.Instance.homeScreen.activeInHierarchy || UIManager.Instance.overScreen.activeInHierarchy)
            {
                if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
                {
                    SoundManager.Instance.menuTheme.Stop();
                }
            }

            UIManager.Instance.ToMap = false;
            UIManager.Instance.SwitchToMap();
        }

        if (UIManager.Instance.ToHangar)
        {
            UIManager.Instance.ToHangar = false;
            UIManager.Instance.SwitchToHangar();
        }

        if (UIManager.Instance.ToOver)
        {
            if (PlayerPrefs.GetInt(Const.MUSIC, 1) == 1)
            {
                SoundManager.Instance.menuTheme.Play();
            }

            UIManager.Instance.ToOver = false;
            UIManager.Instance.SwitchToGameOver();
        }

        gameObject.SetActive(false);
    }
}
