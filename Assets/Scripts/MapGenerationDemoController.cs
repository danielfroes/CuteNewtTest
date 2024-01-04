using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class MapGenerationDemoController : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;
        [SerializeField] MapGenerator _mapGenerator;
        [SerializeField] DemoHud _hud;

        void Start()
        {
            HookEvents();
            _playerController.InjectHeightResolver(_mapGenerator);
            GenerateMap();
        }

        void GenerateMap()
        {
            _mapGenerator.GenerateMap();
        }

        void HookEvents()
        { 
            _hud.OnRegenerateMapClicked += GenerateMap;
        }

        void UnhookEvents()
        {
            _hud.OnRegenerateMapClicked -= GenerateMap;
        }

        void OnDestroy()
        {
            UnhookEvents();
        }

    }
}
