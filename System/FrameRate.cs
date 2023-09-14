//このコードは目標フレームレートを制御します。
//GameManagerにアタッチして使います。
//デバッグモードではInspector上で現在のフレームレートを確認できます。

using UnityEngine;

public class FrameRate : MonoBehaviour
{
    //デバッグモード
    [SerializeField] bool _debugMode = false;
    //デバッグ用カウンター
    [SerializeField] float FPS;

    //ゲームのフレームレート
    [SerializeField] int _frameRate = 60;

    //FPSの計測間隔
    [SerializeField] float _frameCounterInterval = 0.5f;
    //経過フレーム
    int _frameCount;
    //経過時間
    float _timeCount;

    void Awake()
    {
        //ゲームのフレームレートを変更
        Application.targetFrameRate = _frameRate;
    }

    void Update()
    {
        if (_debugMode)
        {
            //経過フレームを測定する
            _frameCount++;

            //経過時間を測定する
            float time = Time.realtimeSinceStartup - _timeCount;

            if (time >= _frameCounterInterval)
            {
                //フレームレートを計算する
                FPS = _frameCount / time;

                //計測用の数値を代入する
                _frameCount = 0;
                _timeCount = Time.realtimeSinceStartup;
            }
        }
    }
}