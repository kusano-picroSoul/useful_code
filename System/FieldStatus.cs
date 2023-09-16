//このコードはゲームのステータスの外部からの取得や変更に対応します。
//GameManagerにアタッチして使用できます。
//DOTweenのパッケージを使用します。
//変数やメソッドをカスタムして任意のステータスを追加できます。

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FieldStatus : MonoBehaviour
{
    //デバッグモード
    [SerializeField] bool _debugMode = false;
    //デバッグ用ステータス
    [SerializeField] int _AddStatus = 10;

    //各ステータスと初期値の宣言
    [SerializeField] int _health = 100;
    [SerializeField] int _hungry = 100;
    [SerializeField] int _wood = 0;

    //各ステータスの上限値の宣言
    [SerializeField] int _maxHealth = 100;
    [SerializeField] int _maxHungry = 100;
    [SerializeField] int _maxWood = 999999;

    //各ステータスの下限値の宣言
    [SerializeField] int _minHealth = 0;
    [SerializeField] int _minHungry = 0;
    [SerializeField] int _minWood = 0;

    //DOTweenでの変動中に増減する可能性があるステータスの宣言
    int realWood;

    //ステータスを変更する時間
    [SerializeField] float _distance = 0.2f;

    //各ステータスが反映されるUIの宣言
    [SerializeField] GameObject _healthBer;
    [SerializeField] GameObject _hungryBer;
    [SerializeField] Text _woodCount;

    //各ステータスの取得
    public int Health {get{return _health;}}
    public int Hungry {get{return _hungry;}}
    public int Wood {get{return _wood;}}

    void Update()
    {
        //デバッグ用
        if (_debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Q)) HealthUp(_AddStatus);
            else if (Input.GetKeyDown(KeyCode.W)) HealthDown(_AddStatus);
            else if (Input.GetKeyDown(KeyCode.E)) HungryUp(_AddStatus);
            else if (Input.GetKeyDown(KeyCode.R)) HungryDown(_AddStatus);
            else if (Input.GetKeyDown(KeyCode.T)) WoodUp(_AddStatus);
            else if (Input.GetKeyDown(KeyCode.Y)) WoodDown(_AddStatus);
        }
    }

    //各ステータスの増減時の処理
    public void HealthUp(int addHealth)
    {
        //ステータスを増やす
        _health += addHealth;

        //上限値を超えない様にする
        if (_health > _maxHealth) _health = _maxHealth;

        //ステータスバーを増やす
        DOTween.To(() => _healthBer.transform.localScale, (x) => _healthBer.transform.localScale = x, new Vector3(_health / 100f, 1, 1), _distance);
    }
    public void HealthDown(int decHealth)
    {
        //ステータスを減らす
        _health -= decHealth;

        //下限値を超えない様にする
        if (_health < _minHealth) _health = _minHealth;

        //ステータスバーを減らす
        DOTween.To(() => _healthBer.transform.localScale, (x) => _healthBer.transform.localScale = x, new Vector3(_health / 100f, 1, 1), _distance);
    }

    public void HungryUp(int addHungry)
    {
        //ステータスを増やす
        _hungry += addHungry;

        //上限値を超えない様にする
        if (_hungry > _maxHungry) _hungry = _maxHungry;

        //ステータスバーを増やす
        DOTween.To(() => _hungryBer.transform.localScale, (x) => _hungryBer.transform.localScale = x, new Vector3(_hungry / 100f, 1, 1), _distance);
    }
    public void HungryDown(int decHungry)
    {
        //ステータスを減らす
        _hungry -= decHungry;

        //下限値を超えない様にする
        if (_hungry < _minHungry) _hungry = _minHungry;

        //ステータスバーを減らす
        DOTween.To(() => _hungryBer.transform.localScale, (x) => _hungryBer.transform.localScale = x, new Vector3(_hungry / 100f, 1, 1), _distance);
    }

    public void WoodUp(int addWood)
    {
        //変更後の値を計算する
        realWood += addWood;

        //上限値を超えない様にする
        if (realWood > _maxWood) realWood = _maxWood;

        //ステータス表示を増やす
        DOTween.To(() => _wood, (x) => {_wood = x; _woodCount.text = "×" + _wood;}, realWood, _distance);
    }
    public void WoodDown(int decWood)
    {
        //変更後の値を計算する
        realWood -= decWood;

        //下限値を超えない様にする
        if (realWood < _minWood) realWood = _minWood;

        //ステータス表示を増やす
        DOTween.To(() => _wood, (x) => {_wood = x; _woodCount.text = "×" + _wood;}, realWood, _distance);
    }
}