using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Car", menuName ="Scriptables/Car", order =1)]
public class CreateCar : ScriptableObject
{
    public string carName;
    public Sprite carSprite;
}
