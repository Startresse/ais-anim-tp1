using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class IKChain
{
    // Quand la chaine comporte une cible pour la racine. 
    // Ce sera le cas que pour la chaine comportant le root de l'arbre.
    private IKJoint rootTarget = null;

    // Quand la chaine à une cible à atteindre, 
    // ce ne sera pas forcément le cas pour toutes les chaines.
    private IKJoint endTarget = null;

    // Toutes articulations (IKJoint) triées de la racine vers la feuille. N articulations.
    private List<IKJoint> joints = new List<IKJoint>();

    // Contraintes pour chaque articulation : la longueur (à modifier pour 
    // ajouter des contraintes sur les angles). N-1 contraintes.
    private List<float> constraints = new List<float>();


    // Un cylndre entre chaque articulation (Joint). N-1 cylindres.
    //private List<GameObject> cylinders = new List<GameObject>();    



    // Créer la chaine d'IK en partant du noeud endNode et en remontant jusqu'au noeud plus haut, ou jusqu'à la racine
    public IKChain(Transform _endNode, Transform _rootTarget = null, Transform _endTarget = null)
    {
        Debug.Log("=== IKChain::createChain: ===");
        // TODO : construire la chaine allant de _endNode vers _rootTarget en remontant dans l'arbre. 
        // Chaque Transform dans Unity a accès à son parent 'tr.parent'

        endTarget = new IKJoint(_endTarget);

        while (_endNode != null)
        {
            joints.Add(new IKJoint(_endNode));
            if (_endNode == _rootTarget) //root in stuff ??
                break;
            _endNode = _endNode.parent;
        }

        rootTarget = new IKJoint(_rootTarget);

        joints.Reverse();

        for (int i = 0; i < joints.Count - 1; ++i)
        {
            Vector3 diff = (joints[i].position - joints[i + 1].position);
            constraints.Add(diff.magnitude);
        }

    }


    public void Merge(IKJoint j)
    {
        // TODO-2 : fusionne les noeuds carrefour quand il y a plusieurs chaines cinématiques
        // Dans le cas d'une unique chaine, ne rien faire pour l'instant.
    }


    public IKJoint First()
    {
        return joints[0];
    }
    public IKJoint Last()
    {
        return joints[joints.Count - 1];
    }

    public void Backward()
    {
        // TODO : une passe remontée de FABRIK. Placer le noeud N-1 sur la cible, 
        // puis on remonte du noeud N-2 au noeud 0 de la liste 
        // en résolvant les contrainte avec la fonction Solve de IKJoint.
        Last().SetPosition(endTarget.transform.position);

        for (int i = joints.Count - 2; i >= 0; --i)
            joints[i].Solve(joints[i + 1], constraints[i]);
    }

    public void Forward()
    {
        // TODO : une passe descendante de FABRIK. Placer le noeud 0 sur son origine puis on descend.
        // Codez et deboguez déjà Backward avant d'écrire celle-ci.
        First().SetPosition(rootTarget.position);

        for (int i = 1; i < joints.Count; ++i)
            joints[i].Solve(joints[i - 1], constraints[i - 1]);
    }

    public void ToTransform()
    {
        // TODO : pour tous les noeuds de la liste appliquer la position au transform : voir ToTransform de IKJoint
        foreach (IKJoint j in joints)
        {
            j.ToTransform();
        }
    }

    public void Check()
    {
        // TODO : des Debug.Log pour afficher le contenu de la chaine (ne sert que pour le debug)
        foreach (IKJoint j in joints)
        {
            Debug.Log(j.name + j.positionTransform);
        }
        Debug.Log("Target : " + endTarget.name + endTarget.positionTransform);
    }

}
