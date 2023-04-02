using UnityEngine;

public class CriticalHit : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => LevelManager._speed = 0;
}
