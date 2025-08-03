using UnityEngine;

[CreateAssetMenu(fileName = "UserNames", menuName = "Scriptable Objects/UserNames")]
public class UserNames : ScriptableObject
{
    public string[] names;

    public string GetRandomUserName() => names[Random.Range(0, names.Length)];
}
