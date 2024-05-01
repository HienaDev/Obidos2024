using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Bird", fileName = "Bird")]
public class Bird :ScriptableObject
{
    // sprite
    public Sprite sprite;
    public string animationTrigger;
    // description
    public string species;
    public string description;
    public List<string> habits;
    public float height;

    // crime
    public string[] crimes;
    // crime description
    public string[] crimesDescription;
}
