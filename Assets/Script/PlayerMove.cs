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

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>(); 
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnim.Attack();
        }
        
    }

    public void Jumping(float force)
    {

    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        float forceY = 0f;
        if(move != 0)
        {
            System.Console.WriteLine();
        }
        //_grounded = IsGrounded();
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


    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }

        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            print(gameObject.name + " OnCollisionEnter2D voi " + collision.gameObject.name);
            _playerAnim.Hit();
            _health -= 0;
            if (_health <= 0)
            {
                Destroy(this.gameObject);
                Application.LoadLevel("GameOver");
            }
        }
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            //_swordArcSprite.flipX = false;
            //_swordArcSprite.flipY = false;
            //Vector3 newPos = _swordArcSprite.transform.localPosition;
            //newPos.x = 0.4f;
            //_swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            //_swordArcSprite.flipX = true;
            //_swordArcSprite.flipY = true;
            //Vector3 newPos = _swordArcSprite.transform.localPosition;
            //newPos.x = -1.01f;
            //_swordArcSprite.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.6f);
        _resetJump = false;
    }
}
