using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using DG.Tweening;

public class AvatarChangePresenter : MonoBehaviour
{
    public AvatarChangeView View;
    public AvatarChangeModel Model;
    
    private async void Start()
    {
        AddSceneCloseEvent();
        
        var button1Count = 0;
        var button1partsList = Model.AvaterBase[0].GetComponentsInChildren<SkinnedMeshRenderer>();

        View.ClothChengeButton1.AddOnClickEventListener(async () => {
            if(button1Count == -1)
            {
                View.AvatarView.RemoveAllCloth();
            } else
            {
                await View.ChangeClothAsync(button1partsList[button1Count]);
            }

            button1Count++;
            if (button1partsList.Length <= button1Count) { button1Count = -1; };
        });


        var button2Count = 0;
        SkinnedMeshRenderer[] button2partsList = Model.AvaterBase[1].GetComponentsInChildren<SkinnedMeshRenderer>();
        
        View.ClothChengeButton2.AddOnClickEventListener(async () => {
            if (button2Count == -1)
            {
                View.AvatarView.RemoveAllCloth();
            }
            else
            {
                await View.ChangeClothAsync(button2partsList[button2Count]);
            }

            button2Count++;
            if (button2partsList.Length <= button2Count) { button2Count = -1; };
        });
        

        var button3Count = 0;
        SkinnedMeshRenderer[] button3partsList = Model.AvaterBase[2].GetComponentsInChildren<SkinnedMeshRenderer>();
        var animator = Model.AvaterBase[2].GetComponent<Animator>();

        View.ClothChengeButton3.AddOnClickEventListener(() => {
            if (button3Count == -1)
            {
                View.AvatarView.RemoveAllCloth();
            }
            else
            {
                View.AvatarView.SetCloth(button3partsList[button3Count], animator);
            }

            button3Count++;
            if (button3partsList.Length <= button3Count) { button3Count = -1; };
        });


        await new WaitForSeconds(3f);

        View.AvatarView.HideAllRenderer();
    }
    
    private void AddSceneCloseEvent()
    {
        View.CloaseButton.onClick.AddListener(async () => {
            await SceneUtil.CloseActiveScene();
        });
    }
}
