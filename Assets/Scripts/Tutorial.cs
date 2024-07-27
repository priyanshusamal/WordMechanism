using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WordRunner
{
    public class Tutorial : MonoBehaviour
    {

        public GameObject tutorial;

        // Start is called before the first frame update
        void Start()
        {
            tutorial.SetActive(true);
            StartCoroutine("Tutorial_1");
        }

        public IEnumerator Tutorial_1()
        {
            yield return new WaitForSeconds(2);
            tutorial.SetActive(false);
        }

    }
}
