//このコードはクリックエフェクトを制御します。
//EffectManagerにアタッチして使います。
//EffectManagerとは別に、非アクティブのクリックエフェクトが子オブジェクトとして必要です。
//エフェクトを最前面に表示する場合、UIとエフェクトとそれ以外、それぞれを表示する専用のカメラがあると良いです。

using System.Collections;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    //エフェクトカメラ
    [SerializeField] Camera _effectCamera;

    //押した瞬間に出る非アクティブのクリックエフェクト
    [SerializeField] GameObject _clickEffect;
    //押し続けた時に出る非アクティブのクリックエフェクト
    [SerializeField] GameObject _dragEffect;

    //エフェクトが出る秒数
    [SerializeField] float _effectTime = 1.0f;

    //エフェクトのコルーチン
    Coroutine _effectCoroutine;

    //マウスの位置を格納する変数
    Vector3 _mousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_clickEffect)
            {
                 //押した瞬間に出るエフェクトをアクティブにする
                _clickEffect.SetActive(true);

                //数秒後にエフェクトを非アクティブにする
                if (_effectCoroutine != null) StopCoroutine(_effectCoroutine);
                _effectCoroutine = StartCoroutine(ClickEffectDestroy());
            }

            //押し続けた時に出るエフェクトをアクティブにする
            if (_dragEffect) _dragEffect.SetActive(true);
        }

        //ボタンを離したらエフェクトを非アクティブにする
        if (Input.GetMouseButtonUp(0) & _dragEffect) _dragEffect.SetActive(false);

        //マウスカーソルの位置を取得する
        _mousePosition = Input.mousePosition;

        //マウスカーソルの位置をワールド座標に変換してエフェクトの位置を重ねる
        _mousePosition.z = 1;
        transform.position = _effectCamera.ScreenToWorldPoint(_mousePosition);
    }

    //エフェクトを非アクティブにする
    IEnumerator ClickEffectDestroy()
    {
        yield return new WaitForSeconds(_effectTime);
        _clickEffect.SetActive(false);
    }
}