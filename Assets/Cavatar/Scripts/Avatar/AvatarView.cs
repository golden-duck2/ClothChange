using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarView : MonoBehaviour
{
    public Transform MeshRoot;

    public Animator Animator;
    public Transform rootBone;
    public Transform meshRoot;

    [SerializeField] Material bloomMat;

    private Dictionary<string, Transform> boneList = new Dictionary<string, Transform>();

    private Dictionary<HumanBodyBones, Transform> animatorBoneList = new Dictionary<HumanBodyBones, Transform>();

    private List<SkinnedMeshRenderer> settedCloth = new List<SkinnedMeshRenderer>();

    private int layerIndex;

    private void Awake()
    {
        foreach (var tran in this.transform.GetComponentsInChildren<Transform>()) {
            boneList.Add(tran.name, tran);
        }
        
        foreach (var bone in AnimationUtil.HumanBodyBonesArray)
        {
            animatorBoneList.Add(bone, Animator.GetBoneTransform(bone));
        }

        layerIndex = this.gameObject.layer;
    }


    public void HideAllRenderer()
    {
        var renderers = Animator.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    /// <summary>
    /// bone が完全に一緒な場合の 衣装移植機能
    /// </summary>
    /// <param name="mesh"></param>
    public void SetCloth(SkinnedMeshRenderer mesh)
    {
        var newMesh = Instantiate(mesh, meshRoot) as SkinnedMeshRenderer;
        settedCloth.Add(newMesh);

        Transform[] bones = new Transform[newMesh.bones.Length];

        for (int i = 0; i < newMesh.bones.Length; i++)
        {
            bones[i] = boneList[newMesh.bones[i].transform.name];
        }
        newMesh.bones = bones;

        AddBloomMat(newMesh);

        newMesh.gameObject.layer = layerIndex;

        if (mesh.rootBone != null)
        {
            newMesh.rootBone = rootBone;
        }
    }

    /// <summary>
    /// メッシュレンダラーをhuman bone からつける場合
    /// </summary>
    public void SetCloth(SkinnedMeshRenderer mesh, Animator meshedAnimator)
    {
        var newMesh = Instantiate(mesh, meshRoot) as SkinnedMeshRenderer;
        settedCloth.Add(newMesh);

        Transform[] bones = new Transform[newMesh.bones.Length];


        // Animator に紐づいている全てのTransformを取得。これは HumanBodyBones と同じ並びになっている
        var list = meshedAnimator.GetAllBoneTransform(mesh.bones);

        if (list.isAllFind == false)
        {
            Debug.Log("!!!!  メッシュに含まれているTransformの一部がHumanoidに含まれていません。 !!!! ");
        }

        foreach (var pair in list.pairList)
        {
            bones[pair.Index] = Animator.GetBoneTransform(pair.HumanBoneEnum);
        }
        newMesh.bones = bones;


        AddBloomMat(newMesh);


        newMesh.gameObject.layer = layerIndex;

        if (mesh.rootBone != null)
        {
            newMesh.rootBone = rootBone;
        }
    }

    public void RemoveAllCloth()
    {
        foreach(var cloth in settedCloth)
        {
            GameObject.Destroy(cloth.gameObject);
        }
        settedCloth.Clear();
    }

    private void AddBloomMat(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        var newMat = new Material[skinnedMeshRenderer.materials.Length + 1];
        
        for (int i = 0; i < newMat.Length - 1 ; i++)
        {
            newMat[i] = skinnedMeshRenderer.materials[i];
        }
        newMat[newMat.Length - 1 ] = bloomMat;

        skinnedMeshRenderer.materials = newMat;
    }
}
