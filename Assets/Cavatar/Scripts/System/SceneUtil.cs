using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cavater;
using Asyncoroutine;

public static class SceneUtil
{
    /// <summary>
    /// 投げっぱなし遷移
    /// </summary>
    public static async void AddptiveLoad(SceneNames sceneNames)
    {
        await AddptiveLoadAsync(sceneNames);
    }

    /// <summary>
    /// await で待つ場合
    /// </summary>
    public static async Task AddptiveLoadAsync(SceneNames sceneNames)
    {
        // 遷移エフェクト

        await SceneManager.LoadSceneAsync( sceneNames.ToString() , LoadSceneMode.Additive);

        SceneManager.SetActiveScene( SceneManager.GetSceneByName( sceneNames.ToString() ) );

        // 遷移エフェクト
    }
    
    public static async Task CloseActiveScene()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name );

        // 遷移エフェクト
    }

    public static async Task CloseScene(SceneNames sceneNames)
    {
        await SceneManager.UnloadSceneAsync(sceneNames.ToString() );

        // 遷移エフェクト
    }

}
