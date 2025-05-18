using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

[RequireComponent(typeof(CircleCollider2D))]

public class InstrumentItem : Item
{
    public override void Use()
    {
        AudioManager.instance.StopMusic();
        LinkController.instance.anim.SetTrigger("achievedSomething");
        LinkController.instance.map.Disable();
        AudioManager.instance.PlaySFX(AudioManager.instance.getInstrument);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        transform.position = LinkController.instance.GetPlaceHolderPos();
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("TestMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Use();
        }
    }
}
