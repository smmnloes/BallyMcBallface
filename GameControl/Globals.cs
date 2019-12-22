using UnityEngine;


public class Globals : MonoBehaviour
{
    public static GameObject ball;
    public static Player playerControl;
    public static UIControl uiStats;
    public static LayerMask groundMask;
    public static LayerMask waterMask;
    public static LayerMask staticMask;

    void Start()
    {
        ball = GameObject.Find("Ball");
        playerControl = ball.GetComponent<Player>();
        uiStats = GameObject.Find("UIControl_Gamestats").GetComponent<UIControl>();
        groundMask = LayerMask.GetMask("ground_layer");
        waterMask = LayerMask.GetMask("water");
        staticMask = LayerMask.GetMask("static_objects");
    }
}