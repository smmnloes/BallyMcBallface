using UnityEngine;
using UnityEngine.SceneManagement;


public class Controls : MonoBehaviour
{
    private const KeyCode StandaloneRightControl = KeyCode.RightArrow;
    private const KeyCode StandaloneLeftControl = KeyCode.LeftArrow;
    private const KeyCode StandaloneJumpControl = KeyCode.UpArrow;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Go to menu (Android and Windows)
            SceneManager.LoadScene(0);
        }
        
        
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKey(StandaloneRightControl))
            Globals.playerControl.MoveRight();

        if (Input.GetKey(StandaloneLeftControl))
        {
            Globals.playerControl.MoveLeft();
        }

        if (Input.GetKey(StandaloneJumpControl))
            Globals.playerControl.Jump();
        
  
        if (Debug.isDebugBuild)
        {
            // DEBUG INPUT
            if (Input.GetKeyDown(KeyCode.S))
            {
                Globals.uiStats.ChangeScore(100);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Globals.uiStats.RestartLevel();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Globals.uiStats.ChangeLives(1);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Globals.uiStats.LevelCompleted();
            }
            
            if (Input.GetKey(KeyCode.K))
                Globals.playerControl.Death();
        }

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
        }

        
        // DEBUG
        if (Debug.isDebugBuild)
        {
            if (Input.touchCount == 3)
            {
                Globals.uiStats.ChangeLives(1);
            }
        }
#endif
    }
}