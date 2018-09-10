using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdonLib.Commons
{
    public class PlayViewLog : MonoBehaviour
    {

        [SerializeField]
        bool warning = false;
        [SerializeField]
        bool error = true;
        [SerializeField]
        int log_max;
        private Queue logStack;

        // Use this for initialization
        void Awake()
        {
            logStack = new Queue(log_max);
            Application.logMessageReceived += LogCallback;
        }

        private void LogCallback(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
                return;

            string trace = null;
            StringExtensions.TextColor color = StringExtensions.TextColor.white;

            switch (type)
            {
                case LogType.Warning:
                    if (warning)
                    {
                        // UnityEngine.Debug.XXXの冗長な情報をとる
                        trace = stackTrace.Remove(0, (stackTrace.IndexOf("\n") + 1));
                        color = StringExtensions.TextColor.yellow;
                    }
                    break;
                case LogType.Error:
                case LogType.Assert:
                    if (error)
                    {
                        trace = stackTrace.Remove(0, (stackTrace.IndexOf("\n") + 1));
                        color = StringExtensions.TextColor.red;
                    }
                    break;
                case LogType.Exception:
                    if (error)
                    {
                        trace = stackTrace;
                        color = StringExtensions.TextColor.red;
                    }
                    break;

            }

            // ログの行制限
            if (this.logStack.Count == log_max)
                this.logStack.Dequeue();

            string message = condition.Coloring(color) + " : " + condition;
            this.logStack.Enqueue(message);
        }

        /// 

        /// エラーログ表示
        /// 

        void OnGUI()
        {
            if (this.logStack == null || this.logStack.Count == 0)
                return;

            // 表示領域は任意
            float space = 16f;
            float height = 150f;
            Rect drawArea = new Rect(space, (float)Screen.height - height - space, (float)Screen.width * 0.5f, height);
            GUI.Box(drawArea, "");

            GUILayout.BeginArea(drawArea);
            {
                GUIStyle style = new GUIStyle();
                style.wordWrap = true;
                foreach (string log in logStack)
                    GUILayout.Label(log, style);
            }
            GUILayout.EndArea();
        }

    }
}