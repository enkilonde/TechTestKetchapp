using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour
{

    public static UIDebug _UIDebug;
    string debugText;

    private class line
    {
        public string text;
        public float timer;

        public line(string _text)
        {
            text = _text;
            timer = 5;
        }
    }


    private List<line> lines = new List<line>();
    private GUIStyle style;
    private Rect rect;

    // Start is called before the first frame update
    void Awake()
    {
        _UIDebug = this;
        rect = new Rect(0, Screen.height / 2, Screen.width, Screen.height / 2);

    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].timer -= Time.deltaTime;
            if (lines[i].timer <= 0 || lines.Count > 10)
            {
                lines.RemoveAt(i);
                i--;
                continue;
            }
            else
            {
                sb.Append(lines[i].text + (i != lines.Count - 1 ? "\n" : ""));
            }
        }

        debugText = sb.ToString();
        //text.text = sb.ToString();
    }

    public static void WriteLine(string line, bool toLog = false)
    {
        if (_UIDebug == null || !_UIDebug.gameObject.activeSelf)
            return;

        _UIDebug.lines.Add(new line(line));
        if (toLog)
            Debug.Log(line);
    }



    private void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.LowerLeft;
            style.fontSize = Screen.height / 50;
        }

        GUI.Label(rect, debugText, style);


    }


}
