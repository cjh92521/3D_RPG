using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform poolTrans;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float respwanBound;
    private Queue<Enemy> inPool;
    private List<Enemy> outPool;

    private LayerMask groundLayerMask;

    public System.Action onKillEvent;

    private void Start()
    {
        outPool = new List<Enemy>();
        inPool = new Queue<Enemy>(GetComponentsInChildren<Enemy>());
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        foreach (Enemy enemy in inPool)
            enemy.Setup(this);

        while (inPool.Count > 0)
            Respwan();
    }

    public void Respwan()
    {
        Enemy enemy = inPool.Dequeue();
        outPool.Add(enemy);
        enemy.transform.SetParent(enemyParent);
        enemy.isInPool = false;
        Vector2 rand = Random.insideUnitCircle* respwanBound;
        Vector3 pos = new Vector3(rand.x, 10f, rand.y) + enemyParent.position;
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, float.MaxValue, groundLayerMask))
            pos = hit.point;
        enemy.Respwan(pos);
    }
    public void Dead(Enemy enemy)
    {
        enemy.transform.SetParent(poolTrans);
        enemy.InPool(poolTrans.position);

        inPool.Enqueue(enemy);
        outPool.Remove(enemy);

        StartCoroutine(countDown());
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(10f);
        if (inPool.Count >= 0)
            Respwan();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(enemyParent.position, Vector3.up, respwanBound);
    }
#endif
}
