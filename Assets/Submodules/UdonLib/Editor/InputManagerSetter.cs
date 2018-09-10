#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// InputManagerを自動的に設定してくれるクラス
/// </summary>
public class InputManagerSetter
{


    /// <summary>
    /// インプットマネージャーを再設定します。
    /// </summary>
    [MenuItem("Util/Reset InputManager")]
    public static void ResetInputManager()
    {
        Debug.Log("インプットマネージャーの設定を開始します。");
        InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

        Debug.Log("設定を全てクリアします。");
        inputManagerGenerator.Clear();

        Debug.Log("プレイヤーごとの設定を追加します。");
        for (int i = 0; i < 4; i++)
        {
            AddPlayerInputSettings(inputManagerGenerator, i);
        }

        Debug.Log("グローバル設定を追加します。");
        AddGlobalInputSettings(inputManagerGenerator);

        Debug.Log("インプットマネージャーの設定が完了しました。");
    }

    /// <summary>
    /// グローバルな入力設定を追加する（OK、キャンセルなど）
    /// </summary>
    /// <param name="inputManagerGenerator">Input manager generator.</param>
    private static void AddGlobalInputSettings(InputManagerGenerator inputManagerGenerator)
    {

        // 横方向
        {
            var name = "Horizontal";
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
        }

        // 縦方向
        {
            var name = "Vertical";
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
        }

    }

    /// <summary>
    /// プレイヤーごとの入力設定を追加する
    /// </summary>
    /// <param name="inputManagerGenerator">Input manager generator.</param>
    /// <param name="playerIndex">Player index.</param>
    /// 

    private static void AddPlayerInputSettings(InputManagerGenerator inputManagerGenerator, int playerIndex)
    {
        if (playerIndex < 0 || playerIndex > 3) Debug.LogError("プレイヤーインデックスの値が不正です。");
        string upKey = "", downKey = "", leftKey = "", rightKey = "", attackKey = "";
        GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, out attackKey, playerIndex);
        List<string> buttonCodeList = Enum.GetNames(typeof(UdonObservable.InputRx.GamePad.GamePadObservable.ButtonCode)).ToList();
        int joystickNum = playerIndex + 1;

        // 横方向
        {
            var name = string.Format("Player{0} Horizontal", joystickNum);
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 1));
           // inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, leftKey, rightKey, "", ""));
        }

        // 縦方向
        {
            var name = string.Format("Player{0} Vertical", joystickNum);
            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 2));
           // inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, downKey, upKey, "", ""));
        }


        // 全てのキーの生成
        {
            foreach(var key in buttonCodeList.Select((value,index) => new {value, index})){
                var name = string.Format("Player{0} {1}", joystickNum, key.value);
                var button = string.Format("joystick {0} button {1}", joystickNum,key.index);
                inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, attackKey));
            }
        }
    }

    /// <summary>
    /// キーボードでプレイした場合、割り当たっているキーを取得する
    /// </summary>
    /// <param name="upKey">Up key.</param>
    /// <param name="downKey">Down key.</param>
    /// <param name="leftKey">Left key.</param>
    /// <param name="rightKey">Right key.</param>
    /// <param name="attackKey">Attack key.</param>
    /// <param name="playerIndex">Player index.</param>
    private static void GetAxisKey(out string upKey, out string downKey, out string leftKey, out string rightKey, out string attackKey, int playerIndex)
    {
        upKey = "";
        downKey = "";
        leftKey = "";
        rightKey = "";
        attackKey = "";

        switch (playerIndex)
        {
            case 0:
                upKey = "w";
                downKey = "s";
                leftKey = "a";
                rightKey = "d";
                attackKey = "e";
                break;
            case 1:
                upKey = "i";
                downKey = "k";
                leftKey = "j";
                rightKey = "l";
                attackKey = "o";
                break;
            case 2:
                upKey = "up";
                downKey = "down";
                leftKey = "left";
                rightKey = "right";
                attackKey = "[0]";
                break;
            case 3:
                upKey = "[8]";
                downKey = "[5]";
                leftKey = "[4]";
                rightKey = "[6]";
                attackKey = "[9]";
                break;
            default:
                Debug.LogError("プレイヤーインデックスの値が不正です。");
                upKey = "";
                downKey = "";
                leftKey = "";
                rightKey = "";
                attackKey = "";
                break;
        }
    }
}

#endif