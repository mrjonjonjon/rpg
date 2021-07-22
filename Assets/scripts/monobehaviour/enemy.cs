﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enemy : character
{
    public int damageStrength;
Coroutine damageCoroutine;
 float hitPoints;
// 1
public override IEnumerator DamageCharacter(int damage, float interval)
{
// 2
 while (true)
 {// 3
 StartCoroutine(FlickerCharacter());
 hitPoints = hitPoints - damage;
// 4
 if (hitPoints <= float.Epsilon)
 {
// 5
 KillCharacter();
 SceneManager.LoadScene("lvl2");
 break;
 }
// 6
 if (interval > float.Epsilon)
 {
 yield return new WaitForSeconds(interval);
 }
 else
 {
// 7
 break;
 }
 }
}

public override void ResetCharacter()
{
// 2
 hitPoints = startingHitPoints;
}

private void OnEnable()
{
// 1
 ResetCharacter();
}

void OnCollisionEnter2D(Collision2D collision)
{
// 2
 if(collision.gameObject.CompareTag("Player"))
 {
// 3
 player player = collision.gameObject.GetComponent<player>();
// 4
 if (damageCoroutine == null)
 {
 damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
 }
 }
}

// 1
void OnCollisionExit2D(Collision2D collision)
{
// 2
 if (collision.gameObject.CompareTag("Player"))
 {
// 3
 if (damageCoroutine != null)
 {// 4
 StopCoroutine(damageCoroutine);
 damageCoroutine = null;
 }
 }
}



}

