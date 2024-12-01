using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public static InputReceiver Instance;

    public HumanPlayer humanPlayer;
    public BeastPlayer beastPlayer;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;

        humanPlayer = new HumanPlayer();
        beastPlayer = new BeastPlayer();

        humanPlayer.Human.Enable();
        beastPlayer.Beast.Enable();
    }

    #region BEAST METHODS

    public float GetBeastPlayerDodgeInput()
    {
        return beastPlayer.Beast.Dodge.ReadValue<float>();
    }

    public float GetBeastPlayerMoveDirection()
    {
        return beastPlayer.Beast.Movement.ReadValue<float>();
    }

    public float GetBeastPlayerAttackInput() //if this returns 1 player will attak, if returns 0 it wont;
    {
        return beastPlayer.Beast.Attack.ReadValue<float>();
    }

    public float GetBeastPlayerSecondaryAttackInput()
    {
        return beastPlayer.Beast.SecondaryAttack.ReadValue<float>();
    }

    public float GetBeastPlayerJumpInput()
    {
        return beastPlayer.Beast.Jump.ReadValue<float>();
    }

    

    #endregion  



    #region PLAYER METHODS
    public float GetHumanPlayerMoveDirection()
    {
        return humanPlayer.Human.Movement.ReadValue<float>();
    }
    #endregion

}
