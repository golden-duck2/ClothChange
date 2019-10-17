using UnityEngine;
using System;

public class InputBlocker : SingletonPrefab<InputBlocker>
{
    [SerializeField] InputBlockerView _view = default;

    private int blockCount = 0;

    /*
    public void ShowBlocker()
    {
        // そのうち作る
        throw new NotImplementedException();
    }
    public void HideBlocker()
    {
        // そのうち作る
        throw new NotImplementedException();
    }
    */

    public void ShowNowLoading()
    {
        blockCount++;
        if (blockCount > 1) return;

        _view.SetNowLoading(true);
    }

    public void HideNowLoading()
    {
        blockCount--;
        if (blockCount == 0)
        {
            _view.SetNowLoading(false);
            return;
        }

        if (blockCount < 0)
        {
            Debug.LogError("表示されていないNowlodingの非表示が呼ばれました。");
        }
    }


}
