using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class InputBlockerView : MonoBehaviour
{
    [SerializeField] GameObject _blocker = default;
    [SerializeField] GameObject _nowloading = default;
    [SerializeField] Transform nowLoadingIcon = default;


    private void Awake()
    {
        var tween = nowLoadingIcon.DOLocalRotate(new Vector3(0, 0, 360f), 4f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        nowLoadingIcon.OnEnableAsObservable()
            .Subscribe(_ => {
                tween.Play();
            })
            .AddTo(this);

        nowLoadingIcon.OnDisableAsObservable()
            .Subscribe(_ => {
                tween.Pause();
            })
            .AddTo(this);
    }

    /// <summary>
    /// ブロックだけする場合
    /// </summary>
    /// <param name="isEnable"></param>
    public void SetBlock(bool isEnable)
    {
        _blocker.SetActive(isEnable);
    }

    public void SetNowLoading(bool isEnable)
    {
        _blocker.SetActive(isEnable);
        _nowloading.SetActive(isEnable);
    }

}
