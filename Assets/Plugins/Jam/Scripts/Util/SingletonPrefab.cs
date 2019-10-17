using UnityEngine;

/// <summary>
/// MonoBehaviourを継承したシングルトン
/// </summary>
public class SingletonPrefab<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    private static volatile T instance;

    /// <summary>
    /// 同期オブジェクト
    /// </summary>
    private static object syncObj = new object();

    /// <summary>
    /// インスタンスのgetter/setter
    /// </summary>
    public static T Instance
    {
        get
        {
            // アプリ終了時に，再度インスタンスの呼び出しがある場合に，オブジェクトを生成することを防ぐ
            if (applicationIsQuitting)
            {
                return null;
            }
            // インスタンスがない場合に探す
            if (instance == null)
            {
                instance = FindObjectOfType<T>() as T;

                // 複数のインスタンスがあった場合
                if (FindObjectsOfType<T>().Length > 1)
                {
                    return instance;
                }

                // Findで見つからなかった場合、新しくオブジェクトを生成
                if (instance == null)
                {
                    // 同時にインスタンス生成を呼ばないためにlockする
                    lock (syncObj)
                    {

                        GameObject singleton = Instantiate(Resources.Load(GetPath())) as GameObject;
                        // シングルトンオブジェクトだと分かりやすいように名前を設定
                        singleton.name = typeof(T).ToString() + " (singleton)";

                        instance = singleton.GetComponent<T>();
                        // シーン変更時に破棄させない
                        DontDestroyOnLoad(singleton);
                    }
                }

            }
            return instance;
        }
        // インスタンスをnull化するときに使うのでprivateに
        private set
        {
            instance = value;
        }
    }

    /// <summary>
    /// 作成されたフレーム
    /// </summary>
    public int CreatedFrame { private set; get; }


    private static string GetPath()
    {

        switch (typeof(T).Name)
        {
            //case "LoadingScenePresenter":
            //    return "Singleton/LoadingScenePresenter";
        }

        return $"Singleton/{typeof(T).Name}";
    }


    /// <summary>
    /// アプリが終了しているかどうか
    /// </summary>
    static bool applicationIsQuitting = false;

    void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    protected virtual void Awake()
    {
        CreatedFrame = Time.frameCount;
    }

    // コンストラクタをprotectedにすることでインスタンスを生成出来なくする
    protected SingletonPrefab() { }
}
