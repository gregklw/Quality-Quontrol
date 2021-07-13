using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GW2021
{
    /// <summary>
    /// The score board used to keep track of the player's progress which
    /// depends on the products they put in the bin.
    /// </summary>
    public class ProductScoreboard : MonoBehaviour
    {
        /// <summary>
        /// References to the text objects that represent if money and contract status.
        /// </summary>
        [SerializeField] private Text _moneyAmountText, _contractStatus;

        /// <summary>
        /// References to the sprites used to determine if a product was put
        /// into the correct bin or not.
        /// </summary>
        [SerializeField] private Sprite _correctNotif, _wrongNotif;

        /// <summary>
        /// The indicator that uses the notification sprites to see if the correct
        /// product was put into the correct bin.
        /// </summary>
        [SerializeField] private RawImage _notifDisplay;

        /// <summary>
        /// The warning light that triggers when a product is placed in the wrong bin.
        /// </summary>
        [SerializeField] private AlertLight _wrongProductAlert;

        /// <summary>
        /// The image prefab used to visualize how many lives the player will have.
        /// </summary>
        [SerializeField] private GameObject _firedImagePrefab;

        /// <summary>
        /// The transform used to put the "fired" image prefabs.
        /// </summary>
        [SerializeField] private RectTransform _firedImagesT;

        /// <summary>
        /// The sound that plays when an object is put in the correct bin.
        /// </summary>
        [SerializeField] private AudioSource _correctSound;

        /// <summary>
        /// The number of chances the player has to get wrong until they get fired.
        /// </summary>
        [SerializeField][Range(1, 5)] private int _numberOfChances;

        /// <summary>
        /// The array used to store the instantiated images.
        /// </summary>
        private RawImage[] _firedImages;

        /// <summary>
        /// The total amount of money the player has made.
        /// </summary>
        private int _moneyCount;

        /// <summary>
        /// The number of times the player has failed.
        /// </summary>
        private int _numberOfFails;
        
        /// <summary>
        /// A number used to multiply the amount that a player loses everytime
        /// they put a product in the incorrect bin.
        /// </summary>
        private const int PENALTY_MULTIPLIER = 5;

        private void Start()
        {
            SetUpChances();
        }

        /// <summary>
        /// Determines if the product is put into the correct bin.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="isDefectBin"></param>
        public void JudgeProduct(Product product, bool isDefectBin)
        {
            if (product.IsDefect == isDefectBin)
            {
                CorrectBinEvents(product);
            }
            else
            {
                WrongBinEvents(product);
            }
            UpdateScoreboard();
        }

        /// <summary>
        /// Helper method used to determine what to do when a correct product
        /// is put into the correct bin.
        /// </summary>
        /// <param name="product"></param>
        private void CorrectBinEvents(Product product)
        {
            _correctSound.Play();
            _moneyCount += product.MoneyValue;
            _wrongProductAlert.TurnOffAlert();
            _notifDisplay.texture = _correctNotif.texture;
        }

        /// <summary>
        /// Helper method used to determine what to do when the wrong product
        /// is put into the wrong bin.
        /// </summary>
        /// <param name="product"></param>
        private void WrongBinEvents(Product product)
        {
            _moneyCount -= product.MoneyValue * PENALTY_MULTIPLIER;
            _wrongProductAlert.TurnOnAlert();
            _notifDisplay.texture = _wrongNotif.texture;

            _firedImages[_numberOfFails++].color = Color.red;

            if (_numberOfFails >= _numberOfChances) {
                TriggerLoseSequence();
            }
        }

        /// <summary>
        /// Updates the score board with the current amount of money made by the player.
        /// </summary>
        private void UpdateScoreboard()
        {
            _moneyAmountText.text = $"Projected Earnings: {_moneyCount}";
        }

        /// <summary>
        /// Sets up the amount of chances a player gets to mess up.
        /// </summary>
        private void SetUpChances()
        {
            _firedImages = new RawImage[_numberOfChances];
            for (int i = 0; i < _numberOfChances; i++)
            {
                _firedImages[i] = Instantiate(_firedImagePrefab).GetComponent<RawImage>();
                _firedImages[i].transform.SetParent(_firedImagesT);
                _firedImages[i].transform.localScale = new Vector3(1, 1, 1);
                _firedImages[i].rectTransform.localPosition = new Vector3(0, 0, -16);
            }
        }

        /// <summary>
        /// This method is called when the player loses the game.
        /// </summary>
        private void TriggerLoseSequence()
        {
            for (int i = 0; i < _firedImages.Length; i++)
            {
                _firedImages[i].gameObject.SetActive(false);
            }
            _contractStatus.transform.parent.gameObject.SetActive(true);
            _contractStatus.text = "Contract Status: <color=red>TERMINATED</color>";
        }
    }
}