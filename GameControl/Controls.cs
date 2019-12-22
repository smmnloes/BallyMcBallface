using UnityEngine;


public class Controls : MonoBehaviour
{
    private const KeyCode StandaloneRightControl = KeyCode.RightArrow;
    private const KeyCode StandaloneLeftControl = KeyCode.LeftArrow;
    private const KeyCode StandaloneJumpControl = KeyCode.UpArrow;

    void FixedUpdate()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKey(StandaloneRightControl))
            Globals.playerControl.MoveRight();

        if (Input.GetKey(StandaloneLeftControl))
        {
            Globals.playerControl.MoveLeft();
        }

        if (Input.GetKey(StandaloneJumpControl))
            Globals.playerControl.Jump();

        // DEBUG
        if (Input.GetKey(KeyCode.K))
            Globals.playerControl.Kill();

#endif


#if UNITY_ANDROID
        switch (Input.touchCount)
        {
            case 1:
            {
                if (Input.GetTouch(0).position.x > Screen.width / 2 + (Screen.width / 8)) //right
                    Globals.playerControl.MoveRight();

                if (Input.GetTouch(0).position.x < Screen.width / 2 - (Screen.width / 8)) //left
                    Globals.playerControl.MoveLeft();
                break;
            }
            case 2:
                Globals.playerControl.Jump();
                break;
            case 3:
                //DEBUG
                Globals.uiStats.ChangeLives(1);
                break;
        }
#endif
    }
}