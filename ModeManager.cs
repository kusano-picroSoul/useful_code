//このコードはゲーム画面のモードを制御します。
//オブジェクトにアタッチせずに使用できます。
//Modeの項目をカスタムして任意のモードを追加できます。

public static class ModeManager
{
    //ゲーム画面のモードの格納
    public static Mode _gameMode = Mode.FIELD;
    
    //ゲーム画面のモードの種類
    public enum Mode
    {
        TITLE,
        SCENARIO,
        FIELD,
        BATTLE,
        RESULT,
    }
}