using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControls : MonoBehaviour
{
    public Animator anim;
    public GameObject leftHandTarget;
    public GameObject rightHandTarget;
    public GameObject leftFootTarget;
    public GameObject rightFootTarget;
    public GameObject headTarget;

    public float leftHandWeight = 0f;
    public float rightHandWeight = 0f;
    public float leftFootWeight = 0f;
    public float rightFootWeight = 0f;
    public float headWeight = 0f;

    public void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.transform.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.transform.position);
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTarget.transform.position);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTarget.transform.position);
        anim.SetLookAtPosition(headTarget.transform.position);

        leftHandWeight = anim.GetFloat("leftHandWeight");
        rightHandWeight = anim.GetFloat("rightHandWeight");
        leftFootWeight = anim.GetFloat("leftFootWeight");
        rightFootWeight = anim.GetFloat("rightFootWeight");
        headWeight = anim.GetFloat("handWeight");

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeight);
        anim.SetLookAtWeight(headWeight);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
