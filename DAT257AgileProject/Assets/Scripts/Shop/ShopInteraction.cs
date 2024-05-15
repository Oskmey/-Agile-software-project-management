
using UnityEngine.SceneManagement;

public class ShopInteraction : Ainteractable
{
    public override string text => "Press E to Shop";

    public override void Interact()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene("Shop");
    }
}
