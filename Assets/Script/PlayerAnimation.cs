using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator _anim; 
    void Start()
    {
        _anim = GetComponentInChildren<Animator>(); 
    }

    public void Move (float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        _anim.SetBool("Jumping", jump);
    }

    IEnumerator AnimateJump() 
    {
        _anim.Play("Up" );
        yield return new WaitForSeconds(.5f);
        _anim.Play("Down");
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack"); 
    }

    public void Hit()
    {
        _anim.SetTrigger("Hit");
        Debug.Log("Hit");
    }

    public void Death(bool death)
    {
        _anim.SetBool("Death", death);
    }
     
    void Update()
    {
        
    }
}
