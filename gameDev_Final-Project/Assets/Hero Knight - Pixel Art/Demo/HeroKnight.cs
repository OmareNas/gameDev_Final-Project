using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HeroKnight : MonoBehaviour {

    
    public static event Action onPlayerDamage;
    //public static event Action onPlayerHeal;
    public static event Action onPlayerDeath;
    
    [Header("Health")]
    [SerializeField] private int health = 10;
    [SerializeField] private int MAX_HEALTH = 10;
    
    [Header("Player Onjects")]
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private BoxCollider2D boxCollider2D;

    [Header("Attack")]
    public Transform AttackArea;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;

    [Header("Time Manager")]
    public TimeManager timeManager;

    bool Death = false;

    bool isMove = true;

    bool isPause = true;

    [Header("Audio")]
    [SerializeField] private AudioSource attackAudio;
    //[SerializeField] private AudioSource[] attackAudio;
    [SerializeField] private AudioSource dieAudio;
    [SerializeField] private AudioSource jumpAudio;

    [Header("Text Elements")]
    public int points = 0;


    [Header("Events")]
    public UnityEvent<int> pointEvent;

    bool isUpdate = true;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        Death=false;
        isMove = true;
        health=MAX_HEALTH;

        if(PlayerPrefs.GetInt("set first time point") == 0)
        {
            PlayerPrefs.SetInt("set first time point",1);
            points = 0;
        }
        else
        {
            points = PlayerPrefs.GetInt("Player Score");
        }

    }

    // Update is called once per frame
    void Update ()
    {
        if(isMove)
        {
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            // Increase timer that checks roll duration
            if(m_rolling)
                m_rollCurrentTime += Time.deltaTime;

            // Disable rolling if timer extends duration
            if(m_rollCurrentTime > m_rollDuration)
                m_rolling = false;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                m_facingDirection = 1;
                
                //change attackRange direction
                AttackArea.transform.localPosition = new Vector3(0.2f, 0.43f , 0f);
            }
                
            else if (inputX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                m_facingDirection = -1;
                //change attackRange direction
                AttackArea.transform.localPosition = new Vector3( -0.2f, 0.43f , 0f );
            }

            // Move
            if (!m_rolling )
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            // -- Handle Animations --
            //Wall Slide
            m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
            m_animator.SetBool("WallSlide", m_isWallSliding);

            
            //Death
            //if (Input.GetKeyDown("e") && !m_rolling)
            if (Death && !m_rolling)
            {
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
                Debug.Log("we are death?");
                Death=false;
                
            }
    
            //Hurt
            else if (Input.GetKeyDown("q") && !m_rolling)
                m_animator.SetTrigger("Hurt");

            //Attack
            else if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
            {
                Attack();
            }

            // Block
            else if (Input.GetMouseButtonDown(1) && !m_rolling)
            {
                m_animator.SetTrigger("Block");
                m_animator.SetBool("IdleBlock", true);
            }

            else if (Input.GetMouseButtonUp(1))
                m_animator.SetBool("IdleBlock", false);

            // Roll
            else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
            {
                m_rolling = true;
                m_animator.SetTrigger("Roll");
                m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            }
                

            //Jump
            else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
            {
                jumpAudio.Play();
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                    if(m_delayToIdle < 0)
                        m_animator.SetInteger("AnimState", 0);
            }
       
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            
            if(isPause)
            {
                timeManager.PauseGame();
                isPause=false;
                isMove=false;
                
            }
            else
            {
                timeManager.ResetTime();
                isPause = true;
                isMove=true;
                Debug.Log("isPause false");

            }
        }

        if(isUpdate)
        {
            PlayerPrefs.SetInt("Player Score", points);
        }

        // if(Input.GetKeyDown(KeyCode.LeftBracket))
        //     Damage(1);
        // if(Input.GetKeyDown(KeyCode.RightBracket))
        //     Heal(1);

    }

    
    
    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
   
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Trap")
            Die();
     }

    public void Die()
    {
        if(!Death)
        {
            Debug.Log("I am Dead!");
            health =0;
            onPlayerDeath?.Invoke();
            // if(m_animator)
            //     m_animator.SetTrigger("Death");

            //StartCoroutine(delayDeath(m_animator.GetCurrentAnimatorStateInfo(0).length));
        Death = true;
        StartCoroutine(delayDeath(.85f));
        }
        
    }


    IEnumerator delayDeath (float _delay)//(bool death)
    {
        // while(Death==false)
        //     yield return null;
        PlayerPrefs.SetInt("set first time point",0);
        isUpdate = false; 
        dieAudio.Play();
        isMove=false;
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        m_body2d.velocity = new Vector2(inputX, inputY);
   
        m_animator.SetTrigger("Death");
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene("MainMenu");
        yield return null;
        m_body2d.mass = 1;
        Destroy(this.gameObject);

    }

    public void Damage(int amnt)
    {
        if(amnt < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");

        health -=amnt;
        onPlayerDamage?.Invoke();

        if (health <= 0)
        {
            health=0;
            //onPlayerDeath?.Invoke();
            Die();
        }
    }

    public void Heal(int amnt)
    {
        if(amnt < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");

        bool isOverMaxHealth = health + amnt > MAX_HEALTH;    

        if(isOverMaxHealth)
            this.health = MAX_HEALTH;
        else
            this.health += amnt;

        onPlayerDamage?.Invoke();
    }

    private void Attack()
    {
        attackAudio.pitch = UnityEngine.Random.Range(.7f,1.3f);
        attackAudio.Play();
        //play animation
        m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        
        //detect enemy
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(AttackArea.position, attackRange, enemyLayers);

        //damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>().getHealth() != 0)
                enemy.GetComponent<Health>().Damage(attackDamage);
        }
    }

    public int getMaxHealth()
    {
        return MAX_HEALTH;
    }
    public int getCurrentHealth()
    {
        return health;
    }
    void OnDrawGizmosSelected() {
        if (AttackArea == null)
            return;

        Gizmos.DrawWireSphere(AttackArea.position, attackRange);
    }

    public void addPoint()
    {
        points+=1;
        pointEvent.Invoke(points);
    }

}
