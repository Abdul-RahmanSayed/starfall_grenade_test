using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HasWeaponLoSToPlayer : ActionNode
{
    public LayerMask layerMask;
    
    protected override void OnStart()
    {
        layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Player") | layerMask;
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        if (!context.SAi.GetRangedWeapon().barrelTransform || !context.aPlayer) return State.Failure;
        var r = new Ray(context.SAi.GetRangedWeapon().barrelTransform.position,
            context.aPlayer.Collider.bounds.center - context.SAi.GetRangedWeapon().barrelTransform.position);
        if (!Physics.Raycast(r, out var hit, Mathf.Infinity, layerMask)) return State.Failure;
        return hit.collider.gameObject == context.aPlayer.gameObject ? State.Success : State.Failure;
    }
}
