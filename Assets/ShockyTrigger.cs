using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockyTrigger : MonoBehaviour
{
    public Material mat;
    private float percentage;

    public float Duration = 1;

    private bool fired = false;

    public bool ShockyFired
    {
        set { fired = value; }  
    }

    private void Start()
    {
        mat.SetFloat("_Percent", 2);
    }

    private void Update()
    {
        if(fired)
        {
            percentage += Time.deltaTime / Duration;
            mat.SetFloat("_Percent", percentage);
            if(percentage >= 2)
            {
                percentage = 0;
                mat.SetFloat("_Percent", 2);
                fired = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            percentage = 0;
            fired = true;
            //ChangePos(0.45f, 0.2f);
        }
    }

    public void ChangePos(float x, float y)
    {
        mat.SetVector("_FocalPoint", new Vector4(x, y, 0, 0));
    }
}
