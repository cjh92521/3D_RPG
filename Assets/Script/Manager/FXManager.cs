using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private FXScript prefab;
    public void Action(Transform trans)
    {
        FXScript fx = Instantiate(prefab,transform);
        fx.Setup(trans);
    }
}
