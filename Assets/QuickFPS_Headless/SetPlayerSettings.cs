using UnityEngine;

[ExecuteInEditMode]
public class SetPlayerSettings : MonoBehaviour
{
    [Tooltip("If active, will keep setting the orientation to the chosen value")]
    public bool EnableSetting = true;

#if UNITY_EDITOR
    public UnityEditor.UIOrientation Orientation = UnityEditor.UIOrientation.LandscapeRight;

    void Update()
    {
        if (EnableSetting)
        UnityEditor.PlayerSettings.defaultInterfaceOrientation = Orientation;
    }
#endif
}

