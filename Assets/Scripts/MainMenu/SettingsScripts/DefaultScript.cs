using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Data;

namespace MainMenu
{
    public class DefaultScript : MonoBehaviour
    {
        [SerializeField]
        private Slider _vfxSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private GameObject _mediumPlane;
        [SerializeField]
        private GameObject _hardPlane;
        [SerializeField]
        private AudioMixer _audioMixer;

        private float _defaultVolume = -60;

        private MeshRenderer _mediumRenderer;
        private MeshRenderer _hardRenderer;

        private string _shaderPropertyName = "_Color";

        // Start is called before the first frame update
        void Start()
        {
            _mediumRenderer = _mediumPlane.GetComponent<MeshRenderer>();
            _hardRenderer = _hardPlane.GetComponent<MeshRenderer>();
        }

        // Will be called if the current object is triggered
        private void OnTriggerEnter(Collider other)
        {
            _vfxSlider.value = _defaultVolume;
            _audioMixer.SetFloat("sfxVolume", _defaultVolume);

            _musicSlider.value = _defaultVolume;
            _audioMixer.SetFloat("musicVolume", _defaultVolume);

            _mediumRenderer.material.SetColor(_shaderPropertyName, Color.white);
            _hardRenderer.material.SetColor(_shaderPropertyName, Color.white);

            Debug.Log("Setting is default!");

            FindObjectOfType<PlayerDataManager>().SaveSettings(_defaultVolume, _defaultVolume, 0);
        }
    }
}