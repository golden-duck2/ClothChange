using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationUtil
{
    private static HumanBodyBones[] _humanBodyBonesArray = null;
    public static HumanBodyBones[] HumanBodyBonesArray {
        private set { _humanBodyBonesArray = value; }
        get {
            if (_humanBodyBonesArray == null)
            {
                var enumList = (HumanBodyBones[])Enum.GetValues(typeof(HumanBodyBones));

                Array.Resize(ref enumList, enumList.Length - 1);

                _humanBodyBonesArray = enumList;

                return HumanBodyBonesArray;
            } else
            {
                return _humanBodyBonesArray;
            }     
        }
    } 
    
    public static (List<(int Index, HumanBodyBones HumanBoneEnum)> pairList, bool isAllFind) GetAllBoneTransform(this Animator animator, Transform[] exchengeTransforms)
    {
        var ret = new List<(int, HumanBodyBones)>();

        var findCount = 0;

        // Humanbone に属するすべてのTransformを検索する
        for (var i = 0; i < HumanBodyBonesArray.Length; i++)
        {
            // animator からボーンを取得する
            var humanBoneTransform = animator.GetBoneTransform(HumanBodyBonesArray[i]);

            // null の可能性がある
            if (humanBoneTransform == null) continue;

            // チェック元のTransform から検索する
            for (var j = 0; j < exchengeTransforms.Length; j++)
            {
                // 渡されたTransformと同じかどうか
                if (exchengeTransforms[j] == humanBoneTransform)
                {
                    ret.Add( (i, (HumanBodyBones)i) );
                    findCount ++;
                    break;
                }
            }
        }
        //Debug.Log(ret.Count);
        return (ret, (ret.Count == (exchengeTransforms.Length + 1)));
    }
}
