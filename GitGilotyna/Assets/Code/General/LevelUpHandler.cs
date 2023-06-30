using System;
using System.Collections.Generic;
using Code.Enemy.WeaponTypes.WeaponDecorators;
using UnityEngine;
using UnityEngine.UI;

namespace Code.General
{
    public class LevelUpHandler : MonoBehaviour
    {
        private Action<WeaponDecoratorType> selectCallback;
        [SerializeField] private List<GameObject> attachmentSlots;
        private List<WeaponDecoratorType> _attachments;
        [SerializeField] private GameObject upgradePanel;
        public void ShowAttachments(List<WeaponDecoratorType> attachments, Action<WeaponDecoratorType> selectCallback)
        {
            _attachments = attachments;
            for (int i = 0; i < attachments.Count; i++)
            {
                var button = attachmentSlots[i].GetComponent<Button>();
                button.onClick = new();
                var sprite = 
                    Resources.Load<Sprite>($"AttachmentImages/{WeaponDecorator.GetResourceFromType(attachments[i])}");
                button.GetComponentInChildren<ImageChanger>().ChangeSprite(sprite);
                AttachEvent(button, i, selectCallback);
            }
            upgradePanel.SetActive(true);
        }

        private void AttachEvent(Button btn, int index, Action<WeaponDecoratorType> selectCallback)
        {
            btn.GetComponent<Button>().onClick.AddListener(()=>
            {
                selectCallback.Invoke(_attachments[index]);
                upgradePanel.SetActive(false);
            });
        }
    }
}