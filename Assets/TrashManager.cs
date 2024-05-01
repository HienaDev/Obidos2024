using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{

    public static TrashManager instance;

    private void Awake()
    {
        instance = this;
    }


}
