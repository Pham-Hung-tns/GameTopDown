using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private CanvasGroup fade;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
            StartCoroutine(IELoadDungeon());
        }
    }

    public IEnumerator IELoadDungeon()
    {
        fade.gameObject.SetActive(true);
        StartCoroutine(Helper.IEFade(fade, 1f, 2f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(Helper.IEFade(fade, 0f, 1f));
        SceneManager.LoadScene("DungeonScene");
    }
}
