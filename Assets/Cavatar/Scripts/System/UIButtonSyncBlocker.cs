using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSyncBlocker : MonoBehaviour
{
    /// <summary>
    /// Intaractable が変更されるときに使用する Static な callback
    /// </summary>
    public static Action<bool> IntaractableSyncEvent;

    /// <summary>
    /// 個々に保持するUI
    /// </summary>
    [SerializeField] Button _SyncUIButton;

    [SerializeField] int _blockingTime = 1000;

    [SerializeField] bool _isSyncPointerUp = true;

    private bool _dontChengeFlag = false;

    private void Reset()
    {
        _SyncUIButton = GetComponent<Button>();
    }

    private void Awake()
    {
        IntaractableSyncEvent += SyncIntaractable;

        _SyncUIButton.onClick.AddListener(OnClickEvent);
    }

    private void OnDestroy()
    {
        IntaractableSyncEvent -= SyncIntaractable;
    }
    
    private void SyncIntaractable(bool isEnable)
    {
        if (isEnable)
        {
            // Enableのイベントが来た場合でも、フラグが立っている場合は元には戻さない
            if (_dontChengeFlag)
            {
                _dontChengeFlag = false;
                return;
            }
        } else
        {
            // 既に触れられない状態でコールバックが呼ばれた場合、変更対象から外す
            if (_SyncUIButton.interactable == false)
            {
                _dontChengeFlag = true;
                return;
            }
        }

        _SyncUIButton.interactable = isEnable;
    }
    
    public async void OnClickEvent()
    {
        // 同一フレームに2回イベントが発生した場合の対策
        if (_SyncUIButton.interactable == false) return;

        IntaractableSyncEvent?.Invoke(false);

        // TODO : Play SE

        await Task.Run( async () =>
        {
            await Task.Delay(_blockingTime);
        });

        IntaractableSyncEvent?.Invoke(true);
    }
}
