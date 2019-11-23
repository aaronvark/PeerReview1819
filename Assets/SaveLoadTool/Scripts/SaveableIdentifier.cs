using UnityEngine;

namespace Common.SaveLoadSystem
{
    public class SaveableIdentifier : MonoBehaviour, Iidentifier
    {
        public int id;

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = (this.id != 0) ? this.id : id;
        }
    }
}
