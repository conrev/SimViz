using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRHandedGrabInteractable : XRGrabInteractable
{
    [SerializeField]
    private Transform LeftHandAttachTransform;
    [SerializeField]
    private Transform RightHandAttachTransform;



    //  OnSelectEntering - set attachTransform - then call base
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.handedness == InteractorHandedness.Left)
        {
            attachTransform = LeftHandAttachTransform;
        }
        else if (args.interactorObject.handedness == InteractorHandedness.Right)
        {
            attachTransform = RightHandAttachTransform;
        }

        base.OnSelectEntering(args);
    }
}


