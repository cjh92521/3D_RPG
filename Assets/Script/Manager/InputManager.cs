using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMouseClick
{
    void OnLeftMouseClick();
    void OnRightMouseClick();
    void OnMouseOver();
}

public class InputManager : MonoBehaviour
{
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

    static public InputManager Instance { get; private set; }

    [SerializeField] private Texture2D[] cursorTextures;
    [SerializeField] private float clickDistance;
    public PlayerCon player;
    public CamCon cam;

    private Dictionary<KeyCode, List<System.Action>> shortKeys;

    private bool isMouseLeft;
    private bool isMouseRight;

    private IMouseClick target;
    private CursorType cursorType;
    
    private void Awake()
    {
        Instance = this;

        shortKeys = new Dictionary<KeyCode, List<System.Action>>();
    }
    public void AddKeyAction(KeyCode keyCode,System.Action onKeydown)
    {
        if (!shortKeys.ContainsKey(keyCode))
            shortKeys.Add(keyCode, new List<System.Action>());

        shortKeys[keyCode].Add(onKeydown);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            KeyBoardInput();
            MouseInput();
        }
        else
            player.SetAnimator(0, 0);

        CheckTarget();
    }
    private void CheckTarget()
    {
        target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue);
        if (hits.Count() >= 0)
        {
            var targets = hits
                .Select(x => x.collider.gameObject.GetComponent<IMouseClick>())
                .Where(x => x != null);
            if (targets.Count() > 0)
                target = targets.First();
        }

        if (target != null)
            target.OnMouseOver();
        else
            MouseTip.Instance.OffShowUI();
        
    }
    public void ChangeCursorType(CursorType cursorType)
    {
        Cursor.SetCursor(cursorTextures[(int)cursorType],Vector2.zero,0);
    }

    private void KeyBoardInput()
    {
        MovementInput();

        foreach (KeyCode key in shortKeys.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                foreach (System.Action e in shortKeys[key])
                {
                    e?.Invoke();
                }
            }
        }
    }
    private void MovementInput()
    {
        float z = Input.GetAxis("Vertical");
        float x = 0f;
        if (Input.GetKey(KeyCode.Q))
            x = -1;
        else if (Input.GetKey(KeyCode.E))
            x = 1;
        float rorateY = Input.GetAxis("Horizontal");
        player.MoveByKey(x, z, rorateY);
        player.SetAnimator(x,z);

    }
    private void MouseInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        

        if (target != null)
        {
            if (Input.GetMouseButtonDown(0))
                target.OnLeftMouseClick();
            if (Input.GetMouseButtonDown(1))
                target.OnRightMouseClick();
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
                player.MoveByMouse();
            else if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
                cam.CamMoveByMouse();
        }
    }

    public void OnTarget(NonPlayer target)
    {
        player.OnTarget(target);
    }


}
