using System;
using UnityEngine;
using UnityEngine.UI;

namespace CuteNewtTest.MapGeneration
{
    public class DemoHud : MonoBehaviour
    {
        public event Action OnRegenerateMapClicked;
        
        [SerializeField] Button _regenerateButton;


        public void Start()
        {
            _regenerateButton.onClick.AddListener(() => OnRegenerateMapClicked.Invoke());
        }


    }
}