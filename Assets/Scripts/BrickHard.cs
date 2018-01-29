using UnityEngine;
using System.Collections;

public class BrickHard : BrickBase
{
    public Material[] materials;

    private int life;

    protected override void Start()
    {
        base.Start();
        life = materials.Length;
        GetComponent<MeshRenderer>().material = materials[life - 1];
    }

    protected override void BrickEffect()
    {
        life--;
        if (life > 0)
            GetComponent<MeshRenderer>().material = materials[life - 1];
        else
            base.BrickEffect();
    }
}
