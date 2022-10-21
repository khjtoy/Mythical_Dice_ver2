using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockyTrigger : MonoBehaviour
{
    public Material mat;
    private float percentage;

    public float Duration = 1;

    private bool fired = false;

    private void Update()
    {
        if(fired)
        {
            percentage += Time.deltaTime / Duration;
            mat.SetFloat("_Percent", percentage);
            if(percentage > 1)
            {
                percentage = 0;
                fired = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            percentage = 0;
            fired = true;
        }
    }
}
