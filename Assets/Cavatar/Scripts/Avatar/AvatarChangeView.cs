using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System.Threading.Tasks;
using DG.Tweening;
using Asyncoroutine;

public class AvatarChangeView : MonoBehaviour
{
    public Animator animator = default;
    public AvatarView AvatarView = default;

    public Button CloaseButton = default;

    public AvatarPartsButtonBase ClothChengeButton1 = default;
    public AvatarPartsButtonBase ClothChengeButton2 = default;
    public AvatarPartsButtonBase ClothChengeButton3 = default;
    public AvatarPartsButtonBase ClothChengeButton4 = default;

    public PostProcessVolume PostProcessVolume = default;

    [SerializeField] Material _bloomMat;
    

    [System.Serializable]
    public class ClothChengeTime
    {
        public float bloomTotalTime          = 0.4f;
        public float materialEmissionTime   = 0.2f;
        public float keepTime               = 1f;

        public float waitTime1
        {
            get { return bloomTotalTime - materialEmissionTime; }
        }
    }

    ClothChengeTime _time = new ClothChengeTime();


    public async Task ChangeClothAsync(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        DOTween.To(() => 0f, (n) => PostProcessVolume.weight = n, 1f, _time.bloomTotalTime).SetEase(Ease.InExpo);

        await new WaitForSeconds(_time.waitTime1);

        DOTween.To(() => 0f, (n) => _bloomMat.SetFloat("_Intensity", n) , 1f, _time.materialEmissionTime);

        await new WaitForSeconds(_time.materialEmissionTime);

        AvatarView.SetCloth(skinnedMeshRenderer);
        
        DOTween.To(() => 1f, (n) => PostProcessVolume.weight = n            , 0f, _time.keepTime).SetEase(Ease.OutExpo);
        DOTween.To(() => 1f, (n) => _bloomMat.SetFloat("_Intensity", n)     , 0f, _time.keepTime).SetEase(Ease.OutExpo);
    }
}
