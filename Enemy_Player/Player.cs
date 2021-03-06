using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _onGround;
    private bool _inWater;
    private bool _onStatic;
    private bool _attached;
    private int _randomNr = 3;
    
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int Closed = Animator.StringToHash("closed");
    private static readonly int Eyebrows = Animator.StringToHash("augenbrauen");
    private GameObject _moveAlong;
    private GameObject _groundCheckChild;

    private Rigidbody2D _rigid;
    private Animator _Eyesanimator;
    private FixedJoint2D _attachJoint;

    public bool isDead;

    public float jumpForce;
    public float sideForce;
    public float sideForceAir;
    public float maxSpeed;
    public float maxSpeedAir;
    
    private GameObject _eyes;

    private ParticleSystem _grassParticles;
    private ParticleSystem _waterParticles;
    private ParticleSystem _staticParticles;
    
    public AudioSource mainAudiosource;
    public AudioSource waterAudiosource;
    public AudioClip jumpSound;
    public AudioClip waterSwim;

    void Start()
    {
        _eyes = GameObject.Find("Eyes");
        _rigid = GetComponent<Rigidbody2D>();
        _Eyesanimator = _eyes.GetComponent<Animator>();
        _attachJoint = GetComponent<FixedJoint2D>();
        _moveAlong = GameObject.Find("MoveAlong");
        _groundCheckChild = GameObject.Find("groundCheckChild");
        _grassParticles = GameObject.Find("GrassParticles").GetComponent<ParticleSystem>();
        _waterParticles = GameObject.Find("WaterParticles").GetComponent<ParticleSystem>();
        _staticParticles = GameObject.Find("StaticParticles").GetComponent<ParticleSystem>();

    }

    void FixedUpdate()
    {
        var position = _groundCheckChild.transform.position;
        _onGround = Physics2D.OverlapCircle(position, 0.3f, Globals.groundMask); 
        _inWater = Physics2D.OverlapCircle(position, 0.3f, Globals.waterMask);
        _onStatic = Physics2D.OverlapCircle(position, 0.3f, Globals.staticMask);
    }

    void Update()
    {
        ControlMoveAlong();

        AnimateEyes();

        ControlEmitters();
    }
    
    void LateUpdate()
    {
        if (_inWater && Mathf.Abs(_rigid.velocity.x) > 1)
        {
            PlayAudio(waterSwim, waterAudiosource); 	
        }
        else
        {
            waterAudiosource.Stop();
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("tooltip"))
        {
            string checkPointName = col.gameObject.name;
            if (!PlayerStats.instance.GetToolTipTriggered(checkPointName))
            {
                Globals.uiStats.ShowTooltip(col.gameObject);
                PlayerStats.instance.SetToolTipTriggered(checkPointName);
            }
            
            //col.gameObject.SetActive(false);
        }
        
        if (col.gameObject.layer == LayerMask.NameToLayer("deathBoundaries"))
        {
            Death();
        }

        if (col.name == "ZIEL" && !isDead)
        {
            Globals.uiStats.LevelCompleted();
        }

        if (col.name == "1Up" && !isDead)
        {
            Globals.uiStats.ChangeLives(1);
            Destroy(col.gameObject);
        }
    }

    private void ControlMoveAlong()
    {
        if ((!_attached && _rigid.angularVelocity > 1) || (_attached && _rigid.velocity.x < -1))
        {
            //Eyes/Particles: Change direction
            _moveAlong.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if ((!_attached && _rigid.angularVelocity < -1) || (_attached && _rigid.velocity.x > -1))
        {
            _moveAlong.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    private void ControlEmitters()
    {
        var groundGrassEm = _grassParticles.emission;
        var waterPartEm = _waterParticles.emission;
        var staticEm = _staticParticles.emission;


        if (_onGround && Mathf.Abs(_rigid.angularVelocity * Mathf.Deg2Rad * 0.45f) > Mathf.Abs(_rigid.velocity.magnitude))
        {
            groundGrassEm.enabled = true;
        }
        else if (_inWater && Mathf.Abs(_rigid.velocity.x) > 1)
        {
            waterPartEm.enabled = true;
        }
        else if (_onStatic && Mathf.Abs(_rigid.angularVelocity * Mathf.Deg2Rad * 0.45f) >
                 Mathf.Abs(_rigid.velocity.magnitude))
        {
            staticEm.enabled = true;
        }

        else
        {
            groundGrassEm.enabled = false;
            waterPartEm.enabled = false;
            staticEm.enabled = false;
        }
    }

    private void AnimateEyes()
    {
        _Eyesanimator.SetBool(IsDead, isDead);
        // Animate eyes
        switch ((int) Time.time % _randomNr)
        {
            case 0:
                _Eyesanimator.SetTrigger(Closed);
                _randomNr = (int) (Random.Range(1, 6));
                break;
            case 1:
                _Eyesanimator.SetTrigger(Eyebrows);
                _randomNr = (Random.Range(1, 6));
                break;
        }
    }

    
    public void Death()
    {
        if (!isDead) Globals.uiStats.PlayerDied();
        isDead = true;
    }

    public void MoveRight()
    {
        if (_rigid.angularVelocity > -maxSpeed)
            _rigid.AddTorque(-sideForce);

        if (!_onGround)
        {
            //Move while airborne
            if (_rigid.velocity.x < maxSpeedAir)
                _rigid.AddForce(new Vector2(sideForceAir, 0));
        }
    }

    public void MoveLeft()
    {
        if (_rigid.angularVelocity < maxSpeed)
            _rigid.AddTorque(sideForce);

        if (!_onGround)
        {
            if (_rigid.velocity.x > -maxSpeedAir) //Move while airborne
                _rigid.AddForce(new Vector2(-sideForceAir, 0));
        }
    }


    public void Jump()
    {
        if (_onGround || _onStatic)
        {
            _rigid.AddForce(new Vector2(0, jumpForce));
            PlayAudio(jumpSound, mainAudiosource);
        }

        if (_inWater)
        {
            _rigid.AddForce(new Vector2(0, jumpForce * 0.4f));
        }

        if (_attached)
        {
            _attachJoint.enabled = false;
            _rigid.AddForce(new Vector2(0, jumpForce * 1.5f));
            _attached = false;
        }
    }

    static void PlayAudio(AudioClip toPlay, AudioSource source)
    {
        source.clip = toPlay;
        if (!source.isPlaying)
            source.Play();
    }

    public void Attach(Transform attachTo)
    {
        _attachJoint.enabled = true;
        _attachJoint.connectedBody = attachTo.GetComponent<Rigidbody2D>();
        _attachJoint.connectedAnchor = new Vector2(0, -0.5f);
        _attached = true;
    }
}