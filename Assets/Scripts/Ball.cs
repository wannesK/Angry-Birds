using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float releaseBallTime = .15f;
    public float maxDragDistance = 2f;

    public Rigidbody2D hookRigid;
    public GameObject nextBall;

    private bool isPressed = false;
    private Rigidbody2D rigid;
    private SpringJoint2D joint;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hookRigid.position) > maxDragDistance)
            {
                rigid.position = hookRigid.position + (mousePos - hookRigid.position).normalized * maxDragDistance;
            }
            else
            {
                rigid.position = mousePos;
            }            
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
        rigid.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rigid.isKinematic = false;

        StartCoroutine(ReleaseBall());
    }

    IEnumerator ReleaseBall()
    {
        yield return new WaitForSeconds(releaseBallTime);

        joint.enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
