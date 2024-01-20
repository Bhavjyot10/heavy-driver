using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CreateCar car;
    public SpriteRenderer spriteren;
    void Start()
    {
        Debug.Log(car.carName);
        spriteren.sprite = car.carSprite;
    }

    
}
