using UnityEngine;

public class MapLoadCondition : AICondition
{
    public override bool Result()
    {
        return Define.IsMapLoaded;
    }

    public override void SetParam(float param)
    {
        
    }
}