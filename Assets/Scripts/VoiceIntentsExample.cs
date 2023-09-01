using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class VoiceIntentsExample : MonoBehaviour
{
    [SerializeField, Tooltip("Configuration file that holds list of voice commands.")]
    private MLVoiceIntentsConfiguration _voiceConfiguration;

    private readonly MLPermissions.Callbacks permissionCallbacks = new MLPermissions.Callbacks();

    private void Start()
    {
        //Permission Callbacks
        permissionCallbacks.OnPermissionGranted += OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied += OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain += OnPermissionDenied;

        // Requests permissions from the user. 
        MLPermissions.RequestPermission(MLPermission.VoiceInput, permissionCallbacks);
    }

    private void OnPermissionDenied(string permission)
    {
        Debug.Log("Permission Denied!");
    }

    private void OnPermissionGranted(string permission)
    {
        Initialize();
    }

    // Start the voice intent service with the configured voice commands.
    private void Initialize()
    {
        MLVoice.OnVoiceEvent += VoiceEvent;

        if (MLVoice.VoiceEnabled)
        {
            MLResult result = MLVoice.SetupVoiceIntents(_voiceConfiguration);
            if (result.IsOk)
            {
                // Subscribe to the voice command event
                MLVoice.OnVoiceEvent += VoiceEvent;
            }
        }
    }

    // Called when a voice command is detected.
    void VoiceEvent(in bool wasSuccessful, in MLVoice.IntentEvent voiceEvent)
    {
        Debug.Log("Voice!!");
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Append($"<b>Last Voice Event:</b>\n");
        strBuilder.Append($"Was Successful: <i>{wasSuccessful}</i>\n");
        strBuilder.Append($"State: <i>{voiceEvent.State}</i>\n");
        strBuilder.Append($"No Intent Reason\n(Expected NoReason): \n<i>{voiceEvent.NoIntentReason}</i>\n");
        strBuilder.Append($"Event Unique Name:\n<i>{voiceEvent.EventName}</i>\n");
        strBuilder.Append($"Event Unique Id: <i>{voiceEvent.EventID}</i>\n");

        Debug.Log(strBuilder.ToString());

        if (wasSuccessful && voiceEvent.EventName == "Stimulate")
        {
            Debug.Log("Stimulate");
        }
    }

    // Stop the service and disable the event when the script is destroyed.
    private void OnDestroy()
    {
        MLVoice.Stop();
        MLVoice.OnVoiceEvent -= VoiceEvent;

        permissionCallbacks.OnPermissionGranted -= OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied -= OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain -= OnPermissionDenied;
    }
}
