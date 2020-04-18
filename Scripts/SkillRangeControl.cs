using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeControl : MonoBehaviour
{
    private Transform skillRangeCircle;
    public Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        skillRangeCircle = transform.Find("SkillRangeMain").Find("SkillRangeCircle").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillRangeCircle();

    }

    void SkillRangeCircle()
    {
        skillRangeCircle.localPosition = target * 2;
    }

    public Vector2 GetPos()
    {
        return skillRangeCircle.position;
    }
}
