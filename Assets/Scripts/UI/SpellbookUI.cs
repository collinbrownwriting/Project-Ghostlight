using UnityEngine;

namespace GridMaster {
    public class SpellbookUI : MonoBehaviour
    {
    
        public Transform topPanel;
        public GameObject spellbookUI;
        SpellSlot[] slots;

        public void Start () {
            slots = topPanel.GetComponentsInChildren<SpellSlot>();
        }

        public void UpdateUI () {
            for (int i = 0; i < slots.Length; i++) {
                if (i < BookManager.instance.spells.Count){
                    slots[i].AddSpell(BookManager.instance.spells[i]);
                } else {
                    slots[i].ClearSlot();
                }
            }
        }

        public static SpellbookUI instance;
        public static SpellbookUI GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }
    
    }
}