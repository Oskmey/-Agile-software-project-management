
using Codice.Client.BaseCommands;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/Items/mapItemSO")]
    public class mapItemSO : ItemSO, IDestroyableItem
    {
        [SerializeField]
        private Sprite mapSprite;
        [SerializeField]
        private string sceneName;
        [SerializeField]
        private string mapName;

        [SerializeField]
        private AccessorySO accessory;
        public AccessorySO Accessory => accessory;

        public string MapName => mapName;
        public string SceneName => sceneName;
        public Sprite MapSprite => mapSprite;
        public void Awake()
        {
            if(accessory != null)
            {
                Name = accessory.AccessoryName;
            }
        }
    }
}