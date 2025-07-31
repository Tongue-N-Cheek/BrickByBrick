using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserNames", menuName = "Scriptable Objects/UserNames")]
public class UserNames : ScriptableObject
{
    public List<string> userNamePrefixes = new List<string>();
    public List<string> userNameAffixes = new List<string>();
    public List<string> userNameSuffixes = new List<string>();

    private string prefix;
    private string affix;
    private string suffix;

    public string GetRandomUserName()
    {
        if (userNamePrefixes != null && userNameAffixes != null && userNameSuffixes != null)
        {
            int prefixIndex = Random.Range(0, userNamePrefixes.Count);
            int affixIndex = Random.Range(0, userNameAffixes.Count);
            int suffixIndex = Random.Range(0, userNameSuffixes.Count);

            prefix = userNamePrefixes[prefixIndex];
            affix = userNameAffixes[affixIndex];
            suffix = userNameSuffixes[suffixIndex];
        }

        return prefix + affix + suffix;
    }
}
