using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void ClickCoinBtn()
    {
        AdManager.ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    PlayerPrefs.SetFloat(Const.COIN, PlayerPrefs.GetFloat(Const.COIN) + 200);
                    UIManager.Instance.totalCoinInHangar.text = PlayerPrefs.GetFloat(Const.COIN).ToString();
                    UIManager.Instance.totalCoinInMap.text = PlayerPrefs.GetFloat(Const.COIN).ToString();
                    UIManager.Instance.totalCoinInOver.text = PlayerPrefs.GetFloat(Const.COIN).ToString();
                    gameObject.SetActive(false);

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

    public void ClickDiamondBtn()
    {
        AdManager.ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    PlayerPrefs.SetFloat(Const.DIAMOND, PlayerPrefs.GetFloat(Const.DIAMOND) + 10);
                    UIManager.Instance.totalDiamondInHangar.text = PlayerPrefs.GetFloat(Const.DIAMOND).ToString();
                    UIManager.Instance.totalDiamondInMap.text = PlayerPrefs.GetFloat(Const.DIAMOND).ToString();
                    UIManager.Instance.totalDiamondInOver.text = PlayerPrefs.GetFloat(Const.DIAMOND).ToString();
                    gameObject.SetActive(false);

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

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

}
