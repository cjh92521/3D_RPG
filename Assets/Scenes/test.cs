using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField] Image image;
    public enum CursorType
    {
        Normal,
        deNormal,
        Attack,
        deAttack,
        Shop,
        deShop,
        Hand,
        deHand,
    }
    [SerializeField] private Texture2D[] cursorTextures;

    private int i = -1;
    private void Update()
    {
        image.transform.position = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
            if (i > cursorTextures.Length)
                return;
            Cursor.SetCursor(cursorTextures[i], Vector2.zero, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = Input.mousePosition;
        Gizmos.DrawSphere(pos, 1f);
    }
}
