using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IKChain;

public class IK : MonoBehaviour
{

    // Le transform (noeud) racine de l'arbre, 
    // le constructeur créera une sphère sur ce point pour en garder une copie visuelle.
    public GameObject rootNode = null;

    // Un transform (noeud) (probablement une feuille) qui devra arriver sur targetNode
    public Transform srcNode = null;

    // Le transform (noeud) cible pour srcNode
    public Transform targetNode = null;

    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;

    // Toutes les chaines cinématiques 
    public List<IKChain> chains = new List<IKChain>();

    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 10;



    void Start()
    {

        if (createChains)
        {
            Debug.Log("(Re)Create CHAIN");

            createChains = false;

            // Keep track of root
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = rootNode.transform.position;
            sphere.GetComponent<Renderer>().material = Resources.Load("Dev_Orange", typeof(Material)) as Material;

            // TODO : 
            // Création des chaines : une chaine cinématique est un chemin entre deux nœuds carrefours.
            // Dans la 1ere question, une unique chaine sera suffisante entre srcNode et rootNode.
            chains.Add(new IKChain(srcNode, rootNode.transform, targetNode));

            // TODO-2 : Pour parcourir tous les transform d'un arbre d'Unity vous pouvez faire une fonction récursive
            // ou utiliser GetComponentInChildren comme ceci :
            // foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())


            // TODO-2 : Dans le cas où il y a plusieurs chaines, fusionne les IKJoint entre chaque articulation.
        }

    }

    void Update()
    {
        if (createChains)
            Start();

        // if (Input.GetKeyDown(KeyCode.I))
        // {
            IKOneStep(true);
        // }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("=== CHECK ===");
            Debug.Log("Chains count=" + chains.Count);
            int chain_nb = 1;
            foreach (IKChain ch in chains)
            {
                Debug.Log("= chain " + chain_nb + " =");
                ch.Check();
                chain_nb++;
            }
        }
    }

    void IKOneStep(bool down)
    {
        int j;

        for (j = 0; j < nb_ite; ++j)
        {
            foreach (IKChain chain in chains)
            {
                // TODO : IK Backward (remontée), appeler la fonction Backward de IKChain 
                // sur toutes les chaines cinématiques.
                chain.Backward();

                // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain
                chain.ToTransform();

                // IK Forward (descente), appeler la fonction Forward de IKChain 
                // sur toutes les chaines cinématiques.
                chain.Forward();

                // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain
                chain.ToTransform();
            }
        }

    }

}
