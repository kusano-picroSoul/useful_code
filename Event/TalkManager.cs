//このコードはテキストウィンドウを制御します。
//ModeManager.csと一緒に使って下さい。
//子オブジェクトに会話内容と名前のTextのUIを配置したテキストボックスにアタッチして使います。
//csvファイルには会話内容n行目,名前をカンマ区切りで表記します。
//会話を終了させる際はcsvに空の行を表記します。
//名前のTextのUI,csvのデータが無い場合も問題なく動作します。
//デバッグモードでは左クリックでデバッグIDから会話を開始、進行できます。
//csvの項目を増やし、Talk()関数内で条件分岐をする事で、声や動作をカスタムで追加できます。
//csvの項目を増やし、TalkComplete()関数内で条件分岐をする事で会話選択肢などをカスタムで追加できます。

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //デバッグモード
    [SerializeField] bool _debugMode = false;
    //デバッグID
    [SerializeField] int _debugID = 0;

    //会話ウィンドウに表示出来る行数
    [SerializeField] int _textLine = 2;
    //会話終了時にウィンドウを非表示にするか
    [SerializeField] bool _windowBreak = true;

    //名前テキストが表示されるUI
    [SerializeField] Text _nameBox;
    //会話テキストが表示されるUI
    [SerializeField] Text _talkBox;

    //テキストが入っているcsvファイル
    [SerializeField] TextAsset _csvFile;
    //csvの中身が入るリスト
    List<string[]> csvDatas = new List<string[]>();
    //表示する予定のテキストのID
    int _talkID = -1;
    //表示する予定のテキスト
    string _allVoice = "";

    //テキストを表示する間隔の時間
    int _textTime = -1;
    //ディレイを大きくする文字の一覧
    [SerializeField] string _delayString = "、。…";
    //句読点のテキストを表示する間隔
    [SerializeField] int _bigDelay = 24;
    //通常の文字のテキストを表示する間隔
    [SerializeField] int _smallDelay = 4;

    //現在表示されているテキストの順番
    int _activeVoiceNum = 0;

    void Awake()
    {
        //csvファイルの読み込み
        StringReader reader = new StringReader(_csvFile.text);
        while (reader.Peek() != -1)
        {
            csvDatas.Add(reader.ReadLine().Split(','));
        }
    }

    void Update()
    {
        //デバッグ用
        if (Input.GetMouseButtonDown(0) & _debugMode)
        {
            if (ModeManager._gameMode == ModeManager.Mode.SCENARIO) Talk(_talkID);
            else if (ModeManager._gameMode == ModeManager.Mode.FIELD) TalkStart(_debugID);
        }
    }

    void FixedUpdate()
    {   
        //次の文字が表示されるシステム
        if (_textTime == 0 & _allVoice != "")
        {
            //1文字追加する
            _talkBox.text += _allVoice.Substring(_activeVoiceNum, 1);

            //文字を追加した後の処理の決定
            if (_talkBox.text.Length < _allVoice.Length)
            {
                //テキストの順番を1つ進める
                _activeVoiceNum++;

                //次の文字が表示される間隔を決める
                if (_delayString.Contains(_allVoice.Substring(_activeVoiceNum, 1))) _textTime = _bigDelay;
                else _textTime = _smallDelay;
            }
            else
            {
                TalkComplete();
            }
        }
        
        //次の文字を表示させるカウントダウン
        if (_textTime != -1)
        {
            _textTime--;
        }
    }
    
    /// <summary>
    /// 会話画面に遷移する時の処理
    /// </summary>
    public void TalkStart(int ID)
    {
        //モードをシナリオモードにする
        ModeManager._gameMode = ModeManager.Mode.SCENARIO;

        //会話ウィンドウを表示する
        gameObject.SetActive(true);

        //talkIDに代入する
        _talkID = ID;

        Talk(ID);
    }

    /// <summary>
    /// 会話ボタンを押した時の処理
    /// </summary>
    void Talk(int ID)
    {
        if (_talkBox.text.Length < _allVoice.Length & _talkBox.text.Length != 0 & _allVoice != "")
        {
            //予定している文字をすべて表示する処理
            _talkBox.text = _allVoice;

            TalkComplete();
        }
        else
        {
            //次の文字列が空白でない場合
            if (csvDatas[ID][0] != "")
            {
                //各欄にテキストを代入
                _allVoice = "";
                for (int i = 0; i < _textLine; i++) _allVoice += csvDatas[ID][i] + "\n";
                if (_nameBox) _nameBox.text = csvDatas[ID][_textLine];

                //アニメーションの再生

                //ボイスの再生

                //テキストの状況のリセット
                _talkBox.text = "";
                _activeVoiceNum = 0;
                _textTime = 0;
            }
            else
            {
                //テキストボックスの非表示
                if (_windowBreak) gameObject.SetActive(false);
                
                //モードをフィールドモードにする
                ModeManager._gameMode = ModeManager.Mode.FIELD;
            }
        }
    }

    /// <summary>
    /// テキストを表示し終えた時の処理
    /// </summary>
    void TalkComplete()
    {
        //通常通り会話が進む場合
        _talkID++;
        _textTime = -1;
    }
}