using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Bird", fileName = "Bird")]
public class Bird : ScriptableObject
{
    // sprite
    public Sprite sprite;
    // description
    public string species;
    public string description;
    public string[] habits;
    public float altura;

    // crime
    public string crime;
    // crime description
    public string crimeDescription;
}
