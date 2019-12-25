using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ParallaxScript : MonoBehaviour
{
    private Renderer _layer1Renderer;
    private Renderer _layer2Renderer;
    private Renderer _layer3Renderer;
    private Renderer _layer4Renderer;
    private Renderer _layer5Renderer;


    private readonly float _speedFactorLayer1 = 4;
    private readonly float _speedFactorLayer2 = 3;
    private readonly float _speedFactorLayer3 = 2;
    private readonly float _speedFactorLayer4 = 1;
    private readonly float _speedFactorLayer5 = 0.5f;

    void Start()
    {
        _layer1Renderer = GameObject.Find("BackgroundLayer1").GetComponent<Renderer>();
        _layer2Renderer = GameObject.Find("BackgroundLayer2").GetComponent<Renderer>();
        _layer3Renderer = GameObject.Find("BackgroundLayer3").GetComponent<Renderer>();
        _layer4Renderer = GameObject.Find("BackgroundLayer4").GetComponent<Renderer>();
        _layer5Renderer = GameObject.Find("BackgroundLayer5").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerSpeed = Globals.ballRigid.velocity.x;

        _layer1Renderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * _speedFactorLayer1 * Time.deltaTime / 1000, 0);
        _layer2Renderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * _speedFactorLayer2 * Time.deltaTime / 1000, 0);
        _layer3Renderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * _speedFactorLayer3 * Time.deltaTime / 1000, 0);
        _layer4Renderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * _speedFactorLayer4 * Time.deltaTime / 1000, 0);
        _layer5Renderer.material.mainTextureOffset +=
            new Vector2(playerSpeed * _speedFactorLayer5 * Time.deltaTime / 1000, 0);
    }
}