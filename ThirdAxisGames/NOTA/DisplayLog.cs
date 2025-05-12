using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class DebugLogViewer : MonoBehaviour
{

    [Header("General Settings")]

    public int maxLogCount = 10;


    [Header("Log Text Colors")]
    public Color NotaNormalLogColor = Color.blue;
    public Color NotaWarningLogColor = Color.yellow;
    public Color NotaCriticLogColor = Color.red;
    public Color UnityEditorLogColor = Color.white;
    private static List<string> logs = new List<string>();
    private static Vector2 scrollPosition;

    public bool ShowLogs = false; 
    public bool showEditorLogs = false; 
    public bool ShowSceneName = false;
    public bool ShowFPS = false;

     [Header("Log Box Settings")]

    public Vector2 boxSize = new Vector2(500, 300);


    [Header("Log Box Position")]
    public float xPos = Screen.width - 500 - 10; 
    public float yPos = 10;

    GUIStyle style = new GUIStyle(GUI.skin.label);

    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if(showEditorLogs){
            logs.Add(logString);

        }
        
        else{
            
            if(logString.Contains("[NOTA]")){
                logs.Add(logString);
            }
            else if(logString.Contains("[CRITIC NOTA]")){
                logs.Add(logString);
            }

            else if(logString.Contains("[WARNING NOTA]")){
                logs.Add(logString);
            }
        }
    }

    void OnGUI()
    {  
        if(ShowLogs){
            DebugScreen();
        }
        else{
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShowLogs = !ShowLogs;
        }
        else return;
    }

    #region Debug Screen

    void DebugScreen(){
        GUI.Box(new Rect(xPos, yPos, boxSize.x, boxSize.y), $"Nota Display Log Screen {(ShowSceneName ? " - " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name : "")} {(ShowFPS ? " - FPS: " + (1.0f / Time.deltaTime).ToString("F2") : "")}");
        

        scrollPosition = GUI.BeginScrollView(
        new Rect(xPos, yPos + 20, boxSize.x, boxSize.y - 20),
        scrollPosition,
        new Rect(0, 0, boxSize.y - 20, logs.Count * 20)
        );

        for (int i = 0; i < logs.Count; i++)
        {
            
            if (logs[i].Contains("[CRITIC NOTA]"))
            {
                GUI.color = NotaCriticLogColor;
            }
            else if (logs[i].Contains("[WARNING NOTA]"))
            {
                GUI.color = NotaWarningLogColor;
            }

            else if(logs[i].Contains("[NOTA]"))
            {
                GUI.color = NotaNormalLogColor;
            }
            else
            {
                GUI.color = UnityEditorLogColor;
            }
            GUI.Label(new Rect(0, i * 20, boxSize.y - 20, 20), logs[i]);
            if(logs.Count > maxLogCount){
                logs.RemoveAt(0);
            }

            bool isLogFull = logs.Count >= maxLogCount;
            if (isLogFull)
            {
                logs.RemoveAt(0);
            }

        }

        GUI.EndScrollView();

    }
}
#endregion