using UnityEngine;

public class FallAnimation : StateMachineBehaviour
{   
    private float _tempSpeed; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _tempSpeed =LevelManager._speed;
        LevelManager._speed = 3;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => LevelManager._speed = _tempSpeed;
}
