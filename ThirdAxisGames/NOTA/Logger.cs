using UnityEngine;

namespace ThirdAxisGames.NotaLog
{
    public static class Logger 
{
    public static void Log(string message){
        Debug.Log("[NOTA]" + message);
    }

    public static void CriticLog(string message){
        Debug.Log("[CRITIC NOTA]" + message);
    }

    public static void WarningNota(string message){
        Debug.Log("[WARNING NOTA]" + message);
    }
}
    
}
