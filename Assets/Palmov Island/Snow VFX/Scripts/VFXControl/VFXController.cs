using UnityEngine;
using UnityEngine.UI;

namespace VFXControl
{
    public class VFXController : MonoBehaviour
    {
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private ParticleSystem[] _vfx;

        private int _vfxIndex;

        private void Start()
        {
            _prevButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(true);

            _vfxIndex = 0;

            UpdateVfxStates();
        }

        public void ShowNextVfx()
        {
            _vfxIndex = (_vfxIndex + 1) % _vfx.Length;
            UpdateVfxStates();
        }

        public void ShowPrevVfx()
        {
            _vfxIndex--;
            if (_vfxIndex < 0)
                _vfxIndex += _vfx.Length;

            UpdateVfxStates();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                ShowPrevVfx();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                ShowNextVfx();
            }
        }

        private void UpdateVfxStates()
        {
            for (var i = 0; i < _vfx.Length; i++)
            {
                var vfx = _vfx[i];
                if (vfx != null)
                    vfx.gameObject.SetActive(i == _vfxIndex);
            }
        }
    }
}