using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserNames", menuName = "Scriptable Objects/UserNames")]
public class UserNames : ScriptableObject
{
    public List<string> userNamePrefixes = new List<string>();
    public List<string> userNameAffixes = new List<string>();
    public List<string> userNameSuffixes = new List<string>();
}
