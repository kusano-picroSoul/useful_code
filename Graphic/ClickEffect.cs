//このコードはクリックエフェクトを制御します。
//EffectManagerにアタッチして使います。
//EffectManagerとは別に、非アクティブのクリックエフェクトが子オブジェクトとしてに必要です。

using System.Collections;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
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
            //各エフェクトをアクティブにする
            _clickEffect.SetActive(true);
            _dragEffect.SetActive(true);

            //数秒後にエフェクトを非アクティブにする
            if (_effectCoroutine != null) StopCoroutine(_effectCoroutine);
            _effectCoroutine = StartCoroutine(ClickEffectDestroy());
        }

        //ボタンを離したらエフェクトを非アクティブにする
        if (Input.GetMouseButtonUp(0)) _dragEffect.SetActive(false);

        //マウスカーソルの位置を取得する
        _mousePosition = Input.mousePosition;

        //マウスカーソルの位置をワールド座標に変換してエフェクトの位置を重ねる
        _mousePosition.z = 1;
        transform.position = Camera.main.ScreenToWorldPoint(_mousePosition);
    }

    //エフェクトを非アクティブにする
    IEnumerator ClickEffectDestroy()
    {
        yield return new WaitForSeconds(_effectTime);
        _clickEffect.SetActive(false);
    }
}