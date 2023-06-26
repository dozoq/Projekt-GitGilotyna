using System;
using System.Collections.Generic;
using Code.Enemy.WeaponTypes.WeaponDecorators;
using Code.General;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Events
{
    public class SelectAttachmentEvent : MonoBehaviour
    {
        private List<WeaponDecoratorType> CopyOfListWithPossibleWeaponAttachments;
        private List<WeaponDecoratorType> ListOfObtainedAttachments;
        [SerializeField] private int possibleUpgradeCount = 3;
        [SerializeField] private LevelUpHandler _levelUpHandler;
        private Player.Player player;

        private void Start()
        {
            CopyOfListWithPossibleWeaponAttachments = new();
            ListOfObtainedAttachments = new();
        }

        public void GetOrUpdateAttachment(GameObject go)
        {
            if (CopyOfListWithPossibleWeaponAttachments.Count == 0) Init(go);
            var levelOptions = new List<WeaponDecoratorType>();
            for (int i = 0; i < possibleUpgradeCount; i++)
            {
                if(Random.Range(0,100)%2==0) levelOptions.Add(GetAttachmentUpgrade());
                else if (ListOfObtainedAttachments.Count < possibleUpgradeCount) levelOptions.Add(GetNewAttachment());
            }
            _levelUpHandler.ShowAttachments(levelOptions, ApplyUpgrade);
        }

        private void ApplyUpgrade(WeaponDecoratorType decorator)
        { 
            if(ListOfObtainedAttachments.Contains(decorator)) player.LevelUpAttachment(decorator);
            else
            {
                player.AddAttachment(decorator);
                ListOfObtainedAttachments.Add(decorator);
            }
        }

        private WeaponDecoratorType GetAttachmentUpgrade()
        {
            if (ListOfObtainedAttachments.Count == 0) return GetNewAttachment();
            return ListOfObtainedAttachments[
                Random.Range(0, ListOfObtainedAttachments.Count - 1)];
        }

        private WeaponDecoratorType GetNewAttachment()
        {
            return CopyOfListWithPossibleWeaponAttachments[
                Random.Range(0, CopyOfListWithPossibleWeaponAttachments.Count - 1)];
        }

        private void Init(GameObject go)
        {
            player = go.GetComponentInParent<Player.Player>();
            if (!player) throw new Exception("Player must be parent of this object");
            CopyOfListWithPossibleWeaponAttachments = player.firstWeaponData.possibleAttachments;
            ListOfObtainedAttachments = new();
        }
    }
}