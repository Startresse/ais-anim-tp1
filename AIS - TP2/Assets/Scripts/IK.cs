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
    public Transform[] srcNode = null;

    // Le transform (noeud) cible pour srcNode
    public Transform[] targetNode = null;

    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;

    // Toutes les chaines cinématiques 
    public List<IKChain> chains = new List<IKChain>();

    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 10;

    public bool cylinder = true;

    void Start()
    {

        if (createChains)
        {
            createChains = false;

            // Keep track of root
            if (cylinder)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = rootNode.transform.position;
            }
            // sphere.GetComponent<Renderer>().material = Resources.Load("Dev_Orange", typeof(Material)) as Material;

            // Attach cylinders
            // createCylinders(rootNode.transform, srcNode[0]);

            // TODO : 
            // Création des chaines : une chaine cinématique est un chemin entre deux nœuds carrefours.
            // Dans la 1ere question, une unique chaine sera suffisante entre srcNode et rootNode.
            // chains.Add(new IKChain(srcNode[0], rootNode.transform, targetNode[0]));

            // TODO-2 : Pour parcourir tous les transform d'un arbre d'Unity vous pouvez faire une fonction récursive
            // for (int i = 0; i < srcNode.Length; ++i)
            foreach (Transform child in rootNode.transform)
                newCreateSubChains(rootNode.transform, child);

            // TODO-2 : Dans le cas où il y a plusieurs chaines, fusionne les IKJoint entre chaque articulation.
            foreach (IKChain chain in chains) // brute force in O(n) but only called on creation so ok...
            {
                foreach (IKChain chainSec in chains)
                {
                    if (chain == chainSec)
                        continue;

                    chain.Merge(chainSec.First());
                    chain.Merge(chainSec.Last());
                }
            }
        }

    }

    private int index = 0;
    void newCreateSubChains(Transform root, Transform child)
    {
        while (child != null && child.childCount == 1)
            child = child.GetChild(0);
        
        if (child.childCount == 0)
        {
            if (root == rootNode.transform)
                chains.Add(new IKChain(child, root, targetNode[index++]));
            else
                chains.Add(new IKChain(child, null, targetNode[index++]));
        }
        else if (child.childCount > 1)
        {
            foreach (Transform children in child.transform)
            {
                newCreateSubChains(child, children);
            }
            if (root == rootNode.transform)
                chains.Add(new IKChain(child, root, null));
            else
                chains.Add(new IKChain(child, null, null));
        }

        if (cylinder)
            createCylinders(root, child);
    }
               

    void createCylinders(Transform parent, Transform child)
    {
        Transform endTarget = child;
        while (endTarget != parent && endTarget.parent != null)
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            IKInterpolCylinder cyl = cylinder.AddComponent<IKInterpolCylinder>();
            cyl.root = endTarget.parent;
            cyl.tail = endTarget;
            endTarget = endTarget.parent;
        }
    }

    void Update()
    {
        if (createChains)
            Start();

        IKOneStep(true);

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
            // TODO : IK Backward (remontée), appeler la fonction Backward de IKChain 
            // sur toutes les chaines cinématiques.
            for (int i = 0; i < chains.Count; ++i)
                chains[i].Backward();

            // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain
            foreach (IKChain chain in chains)
                chain.ToTransform();

            // IK Forward (descente), appeler la fonction Forward de IKChain 
            // sur toutes les chaines cinématiques.
            for (int i = chains.Count - 1; i >= 0; --i)
                chains[i].Forward();

            // TODO : appliquer les positions des IKJoint aux transform en appelant ToTransform de IKChain
            foreach (IKChain chain in chains)
                chain.ToTransform();
        }
    }

}
