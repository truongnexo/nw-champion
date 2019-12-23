using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed;
    [SerializeField]
    public float _health;
    private bool _grounded;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnim.Attack();
        }
        
    } 

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal"); // mũi tên hoặc A - D ( -1 ; 1) không ấn thì 0 
        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _grounded == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
            _grounded = false;
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                _grounded = true;
            } 
        }
    } 
 

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            print(gameObject.name + " OnCollisionEnter2D voi " + collision.gameObject.name);
            _playerAnim.Hit();
            _health -= 50;
            if (_health <= 0)
            {
                StartCoroutine(Death());
                //_playerAnim.Death(true);
                //Destroy(this.gameObject);
                //Application.LoadLevel("Lose");
            }
        }
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 0.1f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -0.1f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.6f);
        _resetJump = false;
    }
    IEnumerator Death()
    {
        _playerAnim.Death(true);
        yield return new WaitForSeconds(0.6f);
        Application.LoadLevel("Lose");
    }
}
