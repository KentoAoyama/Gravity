using UnityEngine;
using UnityEngine.EventSystems;

class MouseClickStop : StandaloneInputModule
{
    //やっていること（多分）
    //
    //StandaloneInputModuleのマウスクリックによる変更以外の処理だけ受け付けるようにしている
    //                                     ↓これ
    //                             ProcessMouseEvent();

    public override void Process()
    {
        //=======moveイベントとは=======
        // Update の度にイベントシステムは呼び出しを受け取り、入力モジュールを検索。
        //どの入力モジュールを今回の更新で使用するかを決定します。そして処理をモジュールに委譲する。


        //戻り値 => update イベントが選択したオブジェクトによって使用された場合
        //選択されているオブジェクトに update イベントを送信
        bool usedEvent = SendUpdateEventToSelectedObject();


        //EventSystemはナビゲーションイベントを有効にすべきか（移動、送信、キャンセル）
        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
            {
                //=======moveイベントとは=======
                //次のSelectable オブジェクトを見つける方向を 4 つの移動方向から決定するやつ


                //戻り値 => move イベントが選択したオブジェクトによって使用された場合
                //選択されているオブジェクトに move イベントを送信
                usedEvent |= SendMoveEventToSelectedObject();

                // ※ |= ← 左辺|右辺の結果を代入する
            }

            if (!usedEvent)
            {
                //=======submitイベントとは=======
                //submitキーが押されたときに呼ばれるイベント


                //戻り値 => submit イベントが選択したオブジェクトによって使用された場合
                //選択されているオブジェクトに submit イベントを送信
                SendSubmitEventToSelectedObject();
            }
        }
    }
}
