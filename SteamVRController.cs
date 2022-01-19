using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR;

public class SteamVRController : XRBaseController
{
    // SteamVR Tracking
    [Header("SteamVR Tracking")]
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Pose poseAction = null;

    // SteamVR Input
    [Header("SteamVR Input")]
    public SteamVR_Action_Boolean selectAction = null;
    public SteamVR_Action_Boolean activateAction = null;
    public SteamVR_Action_Boolean interfaceAction = null;


    private void Start() {
        // Start SteamVR
        SteamVR.Initialize();
        //Debug.Log("input source is " + inputSource);
    }

    protected override void UpdateTrackingInput(XRControllerState controllerState) {
        if (controllerState != null) {
            // Get position from pose action
            Vector3 position= poseAction[inputSource].localPosition;
            controllerState.position = position;
            // Get rotation from position action
            Quaternion rotation = poseAction[inputSource].localRotation;
            controllerState.rotation =rotation;
        }
    }

    protected override void UpdateInput(XRControllerState controllerState) {
        Debug.Log("input source is " + inputSource);
        if (controllerState != null) {
            // Reset all of the input values
            controllerState.ResetFrameDependentStates();
            // Check select action, apply to controller
            SetInteractionState(ref controllerState.selectInteractionState, selectAction[inputSource]);
            // Check activate action, apply to controller
            SetInteractionState(ref controllerState.activateInteractionState, activateAction[inputSource]);
            // Check UI action, apply to controller
            SetInteractionState(ref controllerState.uiPressInteractionState, interfaceAction[inputSource]);
        }
        else {
            Debug.Log("controller state is null");
        }
    }

    private void SetInteractionState(ref InteractionState interactionState, SteamVR_Action_Boolean_Source action) {
        // Pressed this frame
        interactionState.activatedThisFrame = action.stateDown;
        // Released this frame
        interactionState.deactivatedThisFrame = action.stateUp;
        // Is pressed
        interactionState.active = action.state;
        Debug.Log("interaction state is " + action.state);
    }
}