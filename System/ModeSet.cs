//このコードはシーンが読み込まれた時のゲームモードを制御します。
//ModeManager.csと一緒に使って下さい。
//GameManagerにアタッチして使います。
//スクリプト内のシーン名とモードを編集する事で使用して下さい。
//デバッグモードではInspector上で現在のゲームモードを確認できます。

using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSet : MonoBehaviour
{
    //デバッグモード
    [SerializeField] bool _debugMode = false;

    //ゲームモード
    [SerializeField] string _mode;

    void Start()
    {
        //シーン名に応じたゲームモードをセットする。
        if (SceneManager.GetActiveScene().name == "Title") ModeManager._gameMode = ModeManager.Mode.TITLE;
        else if (SceneManager.GetActiveScene().name == "Field") ModeManager._gameMode = ModeManager.Mode.FIELD;
        else if (SceneManager.GetActiveScene().name == "Battle") ModeManager._gameMode = ModeManager.Mode.BATTLE;
        else Debug.LogError("ModeSet内の条件とシーン名が一致しません。");
    }

    void Update()
    {
        if (_debugMode)
        {
            if (ModeManager._gameMode == ModeManager.Mode.TITLE) _mode = "TITLE";
            else if (ModeManager._gameMode == ModeManager.Mode.FIELD) _mode = "FIELD";
            else if (ModeManager._gameMode == ModeManager.Mode.BATTLE) _mode = "BATTLE";
            else Debug.LogError("ModeSet内の条件とモード名が一致しません。");
        }
    }
}
