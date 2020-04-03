using UnityEngine.UI;
using UnityEngine.Events;

public static class ExtensionMethods
{
    public static void AddMethod(this Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    public static void SetMethod(this Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
