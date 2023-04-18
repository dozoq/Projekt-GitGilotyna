using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Quest")]
public class Quest : ScriptableObject
{
    public string Name;
    public int scene;
}
