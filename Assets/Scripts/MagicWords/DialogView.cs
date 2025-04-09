using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VContainer;
namespace Assets.Scripts.MagicWords
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField]
        DialogueUI Dialog;

        [SerializeField]
        Transform DialogParent;

        [SerializeField] 
        GameObject TextPrefab;

        [SerializeField] 
        GameObject EmojiPrefab;

        [SerializeField]
        Material spriteAssetMaterial;

        private const string jsonURL = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";

        private IDialogViewModel _dialogViewModel;

        [Inject]
        private void Constructor(IDialogViewModel dialogViewModel)
        {
            _dialogViewModel = dialogViewModel;
            _dialogViewModel.InitializeAsync(jsonURL);
            _dialogViewModel.SetSpriteAssetMaterial(spriteAssetMaterial);

            _dialogViewModel.DialogueList.ObserveAdd()
            .Subscribe(evt => UpdateDialogueText(evt.Value))
            .AddTo(this);
        }

        private void UpdateDialogueText(Dialogue dialogue)
        {
            DialogueUI obj = Instantiate(Dialog, DialogParent);
            var avatar = _dialogViewModel.GetAvatar(dialogue.name);
            if (avatar != null)
            {
                SetAnchorPosition(obj.Image.rectTransform, avatar.position);
                StartCoroutine(DownloadImage(avatar.url, obj.Image));
            }
            var spriteasst = _dialogViewModel.GetSpriteAsset();
            obj.Line.spriteAsset = spriteasst;
            obj.Line.text = dialogue.text;
        }

        public void SetAnchorPosition(RectTransform rectTransform, string position)
        {
            switch (position.ToLower())
            {
                case "left":
                    rectTransform.anchorMin = new Vector2(0, 0.5f);  // Left
                    rectTransform.anchorMax = new Vector2(0, 0.5f);  // Left
                    rectTransform.anchoredPosition = new Vector2(30, 22);  // Offset (if needed)
                    break;

                case "right":
                    rectTransform.anchorMin = new Vector2(1, 0.5f);  // Right
                    rectTransform.anchorMax = new Vector2(1, 0.5f);  // Right
                    rectTransform.anchoredPosition = new Vector2(-30, 22);  // Offset (if needed)
                    break;

                default:
                    Debug.LogError("Unknown position: " + position);
                    break;
            }
        }

        private IEnumerator DownloadImage(string url, Image avatarImage)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();


                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    avatarImage.sprite = SpriteFromTexture(texture);
                }
                else
                {
                    Debug.LogError("Failed to load avatar: " + uwr.error);
                }
            }
        }
        private Sprite SpriteFromTexture(Texture2D texture)
        {
            return Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }
    }
}

