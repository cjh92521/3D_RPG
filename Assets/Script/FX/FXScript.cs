using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem fx;
    private Transform trans;

    public void Setup(Transform trans)
    {
        this.trans = trans;
        fx.Play();
    }
    void Update()
    {
        transform.position = trans.position;
    }
}
