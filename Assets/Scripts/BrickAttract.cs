using UnityEngine;
using System.Collections;

public class BrickAttract : BrickBase
{
    public GameObject attractPrefab;
    public float attractR;

    private GameObject attract;

    protected override void Start()
    {
        base.Start();
        attract = Instantiate(attractPrefab);
        attract.transform.SetParent(transform.root);
        attract.transform.localScale = new Vector3(attractR, attractR, attractR);
        attract.transform.SetParent(transform);
    }

    protected override void BrickEffect()
    {
        Destroy(attract);
        base.BrickEffect();
    }
}
