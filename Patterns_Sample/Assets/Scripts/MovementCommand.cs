using UnityEngine;

public class MovementCommand : MonoBehaviour, ICommand
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    public bool ShouldMove =>
            InsideCamera ||
            (Player.Instance.HVal > 0F && ReachedLeftBound) ||
            (Player.Instance.HVal < 0F && ReachedRightBound);

    private bool InsideCamera =>
        !ReachedRightBound && !ReachedLeftBound;

    private bool ReachedRightBound =>
        referencePointComponent >= rightCameraBound;

    private bool ReachedLeftBound =>
        referencePointComponent <= leftCameraBound;

    public void Execute()
    {
        if (ShouldMove)
        {
            Player.Instance.transform.Translate(
                transform.right * Player.Instance.HVal * moveSpeed * Time.deltaTime);

            referencePointComponent = Player.Instance.transform.position.x;
        }
    }

    private void Start()
    {
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            0F, 0F, 0F)).x + Player.PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            1F, 0F, 0F)).x - Player.PLAYER_RADIUS;
    }
}