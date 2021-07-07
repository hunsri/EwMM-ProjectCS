using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class InfoScript : MonoBehaviour
    {
        [SerializeField]
        private Text _planeText;

        private bool _planeIsTriggered = false;

        // Start is called before the first frame update
        void Start()
        {
            _planeText.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_planeIsTriggered == true)
            {
                _planeText.text = RandomCovid19Text(Random.Range(0, 11));
                _planeIsTriggered = false;
            }
        }

        // Will return one of many text of covid 19
        private string RandomCovid19Text(int number)
        {
            switch (number)
            {
                case 0:
                    return "The virus is spread through inhalation of airborne droplets sprayed when coughing, sneezing, or talking, as well as through contact with surfaces and subsequent entry into the eyes, nose, or mouth.";
                case 1:
                    return "On March 11, 2020, the spread of the virus was declared a pandemic by the WHO.";
                case 2:
                    return "The virus can remain viable for several hours on surfaces of objects. On steel surfaces and plastic, it can persist for up to 2-3 days.";
                case 3:
                    return "The highest number of antibodies against SARS-CoV-2 is produced two to three weeks after infection, after which their number begins to decrease.";
                case 4:
                    return "For infection caused by SARS - CoV - 2 virus, the incubation period is 1 - 14 days.";
                case 5:
                    return "Symptoms develop on average on day 5-6 from the moment of infection.";
                case 6:
                    return "The COVID-19 vaccination is designed to build up acquired immunity against the SARS-CoV-2 virus by training your own immune system.";
                case 7:
                    return "There is a Corona warning app that helps people to determine whether they have come into contact with an infected person and whether this could result in a risk of infection.";
                case 8:
                    return "The outbreak of COVID-19 was first recorded in Wuhan, China, in December 2019.";
                case 9:
                    return "As of 15 June 2021, more than 177 million cases had been reported worldwide; more than 3.8 million people had died and more than 161 million had recovered.";
                case 10:
                    return "Common symptoms of COVID-19 include fever, cough, fatigue, breathlessness and loss of sense of smell, and possibly stuffy ears. Complications can include acute respiratory distress syndrome and pneumonia.";
                case 11:
                    return "As there are no antiviral drugs to treat the COVID-19, the initial treatment is done with symptomatic therapy.";
                default:
                    return "";
            }
        }

        // Will change the boolean _planeIsTriggered if the object which is bind to the script is triggered
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                _planeIsTriggered = true;
            }
        }
    }
}