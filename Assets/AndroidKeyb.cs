#if UNITY_ANDROID
using UnityEngine.Android;
#endif

using UnityEngine;
using TMPro;
public class AndroidKeyb : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputField;

    private void Start()
    {
        // Request the user's permission to use the Android keyboard
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    public void OnInputFieldSelected()
    {
        // Open the Android keyboard when the TMP Input Field is selected
#if UNITY_ANDROID
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
#endif
    }
}





